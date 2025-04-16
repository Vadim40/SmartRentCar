import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class DateInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const modifiedBody = req.body ? this.convertDatesToUTC(req.body) : req.body;
        const modifiedReq = req.clone({ body: modifiedBody });
        return next.handle(modifiedReq);
    }

    private convertDatesToUTC(body: any): any {
        if (typeof body === 'object') {
            for (const key of Object.keys(body)) {
                if (body[key] instanceof Date) {
                    body[key] = new Date(body[key].getTime() - body[key].getTimezoneOffset() * 60000).toISOString();
                }
            }
        }
        return body;
    }
}
