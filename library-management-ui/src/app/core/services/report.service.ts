import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { firstValueFrom } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ReportService {
  private readonly api = inject(ApiService);

  async download(entity: string, format: 'csv' | 'pdf' | 'xlsx'): Promise<void> {
    const response = await firstValueFrom(this.api.download(`reports/${entity}`, { format }, 'blob')) as Blob;
    const filename = `${entity}-${new Date().toISOString().slice(0, 10)}.${format}`;
    const url = window.URL.createObjectURL(response);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    anchor.click();
    window.URL.revokeObjectURL(url);
  }
}
