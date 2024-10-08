import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout/layout.component';
import { UsersComponent } from './features/users/users/users.component';
import { RolesComponent } from './features/roles/roles/roles.component';
import { GroupsComponent } from './features/groups/groups/groups.component';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children:
            [
                {
                    path: '',
                    redirectTo: 'users',
                    pathMatch: "full",
                },
                {
                    path: 'users',
                    component: UsersComponent
                },
                {
                    path: 'roles',
                    component: RolesComponent
                },
                {
                    path: 'groups',
                    component: GroupsComponent
                }
            ]
    }
];

