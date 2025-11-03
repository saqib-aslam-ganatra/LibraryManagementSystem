using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Reports.DTOs;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LibraryManagement.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        static ReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportFileDto> GenerateAsync(string entity, string format, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entity))
            {
                throw new ArgumentException("Entity must be provided", nameof(entity));
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                throw new ArgumentException("Format must be provided", nameof(format));
            }

            var normalizedEntity = entity.Trim().ToLowerInvariant();
            var normalizedFormat = format.Trim().ToLowerInvariant();

            var (headers, rows, title) = normalizedEntity switch
            {
                "books" => await BuildBookRowsAsync(cancellationToken),
                "members" => await BuildMemberRowsAsync(cancellationToken),
                "loans" => await BuildLoanRowsAsync(cancellationToken),
                "authors" => await BuildAuthorRowsAsync(cancellationToken),
                _ => throw new ArgumentException($"Unsupported entity '{entity}'.", nameof(entity))
            };

            return normalizedFormat switch
            {
                "csv" => CreateCsvReport(title, headers, rows),
                "xlsx" => CreateExcelReport(title, headers, rows),
                "pdf" => CreatePdfReport(title, headers, rows),
                _ => throw new ArgumentException($"Unsupported format '{format}'.", nameof(format))
            };
        }

        private async Task<(IReadOnlyList<string> headers, IReadOnlyList<string[]> rows, string title)> BuildBookRowsAsync(CancellationToken cancellationToken)
        {
            var books = await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToListAsync(cancellationToken);

            var headers = new[] { "Title", "ISBN", "Author", "Total Copies", "Available Copies", "Replacement Cost" };
            var rows = books.Select(book => new[]
            {
                book.Title,
                book.ISBN,
                book.Author?.Name ?? string.Empty,
                book.TotalCopies.ToString(CultureInfo.InvariantCulture),
                book.AvailableCopies.ToString(CultureInfo.InvariantCulture),
                book.ReplacementCost.ToString("C", CultureInfo.InvariantCulture)
            }).ToList();

            return (headers, rows, "Books");
        }

        private async Task<(IReadOnlyList<string> headers, IReadOnlyList<string[]> rows, string title)> BuildMemberRowsAsync(CancellationToken cancellationToken)
        {
            var members = await _context.Members
                .AsNoTracking()
                .OrderBy(m => m.FullName)
                .ToListAsync(cancellationToken);

            var headers = new[] { "Name", "Email", "Phone", "Address" };
            var rows = members.Select(member => new[]
            {
                member.FullName,
                member.Email,
                member.PhoneNumber ?? string.Empty,
                member.Address ?? string.Empty
            }).ToList();

            return (headers, rows, "Members");
        }

        private async Task<(IReadOnlyList<string> headers, IReadOnlyList<string[]> rows, string title)> BuildLoanRowsAsync(CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.Member)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync(cancellationToken);

            var headers = new[] { "Book", "Member", "Loan Date", "Due Date", "Return Date", "Status" };
            var rows = loans.Select(loan => new[]
            {
                loan.Book.Title,
                loan.Member.FullName,
                loan.LoanDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                loan.DueDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? string.Empty,
                loan.ReturnDate?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? string.Empty,
                loan.Status.ToString()
            }).ToList();

            return (headers, rows, "Loans");
        }

        private async Task<(IReadOnlyList<string> headers, IReadOnlyList<string[]> rows, string title)> BuildAuthorRowsAsync(CancellationToken cancellationToken)
        {
            var authors = await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync(cancellationToken);

            var headers = new[] { "Name", "Biography" };
            var rows = authors.Select(author => new[]
            {
                author.Name,
                author.Biography ?? string.Empty
            }).ToList();

            return (headers, rows, "Authors");
        }

        private static ReportFileDto CreateCsvReport(string title, IReadOnlyList<string> headers, IReadOnlyList<string[]> rows)
        {
            static string Escape(string input)
            {
                if (input.Contains('"') || input.Contains(',') || input.Contains('\n'))
                {
                    return $"\"{input.Replace("\"", "\"\"")}\"";
                }

                return input;
            }

            var builder = new StringBuilder();
            builder.AppendLine(string.Join(',', headers.Select(Escape)));

            foreach (var row in rows)
            {
                builder.AppendLine(string.Join(',', row.Select(Escape)));
            }

            var content = Encoding.UTF8.GetBytes(builder.ToString());
            return new ReportFileDto
            {
                FileName = $"{title.ToLowerInvariant()}-report.csv",
                ContentType = "text/csv",
                Content = content
            };
        }

        private static ReportFileDto CreateExcelReport(string title, IReadOnlyList<string> headers, IReadOnlyList<string[]> rows)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Report");

            for (var columnIndex = 0; columnIndex < headers.Count; columnIndex++)
            {
                worksheet.Cell(1, columnIndex + 1).Value = headers[columnIndex];
                worksheet.Cell(1, columnIndex + 1).Style.Font.Bold = true;
            }

            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                for (var columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    worksheet.Cell(rowIndex + 2, columnIndex + 1).Value = row[columnIndex];
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return new ReportFileDto
            {
                FileName = $"{title.ToLowerInvariant()}-report.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Content = stream.ToArray()
            };
        }

        private static ReportFileDto CreatePdfReport(string title, IReadOnlyList<string> headers, IReadOnlyList<string[]> rows)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header().Text($"{title} Report").FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var _ in headers)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        table.Header(header =>
                        {
                            foreach (var headerText in headers)
                            {
                                header.Cell().Element(CellStyle).Text(headerText).SemiBold();
                            }
                        });

                        foreach (var row in rows)
                        {
                            foreach (var cell in row)
                            {
                                table.Cell().Element(CellStyle).Text(cell);
                            }
                        }
                    });

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.Padding(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    }
                });
            });

            return new ReportFileDto
            {
                FileName = $"{title.ToLowerInvariant()}-report.pdf",
                ContentType = "application/pdf",
                Content = document.GeneratePdf()
            };
        }
    }
}
