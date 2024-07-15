import { Routes } from "@angular/router";
import { AdminFeatureComponent } from "./admin-feature.component";
import { AddRoleComponent } from "../add-role/add-role.component";

export const routes: Routes = [
    {
        path: '',
        component: AdminFeatureComponent,
        children: [
            {
                path: 'role',
                component: AddRoleComponent
            }]
    }
];
