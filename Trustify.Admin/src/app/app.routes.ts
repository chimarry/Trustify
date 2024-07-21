import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout/layout.component';
import { UsersComponent } from './features/users/users/users.component';


export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children:
            [
                {
                    path: '',
                    redirectTo: 'dashboard',
                    pathMatch: "full"
                },
                {
                    path: 'dashboard',
                    redirectTo: "users",
                    pathMatch: "full"
                },
                {
                    path: 'users',
                    component: UsersComponent
                },
                {
                    path: 'roles',
                    component: LayoutComponent
                }
            ]
    }
];

