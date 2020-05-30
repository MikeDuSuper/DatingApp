import { Injectable } from '@angular/core';
import { Message } from '../_models/message';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
    pageNumber = 1;
    pageSize = 10;
    messageContainer = 'Unread';

    constructor(private userService: UserService, private router: Router,
                private authService: AuthService, private alertify: AlertifyService){}

        resolve(route: ActivatedRouteSnapshot): Observable<Message[]>{
            return this.userService.getMessages(this.authService.decodeToken.nameid, this.pageNumber, this.pageSize).pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/home']);
                    return of(null);
                })
            );
        }
    }
