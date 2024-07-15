import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout/layout.component';
import { ImageContentComponent } from './main/features/image-content/image-content/image-content.component';
import { TextualContentComponent } from './main/features/textual-content/textual-content/textual-content.component';
import { LargeContentComponent } from './main/features/large-content/large-content/large-content.component';
import { SectionsComponent } from './main/features/sections/sections/sections.component';

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
                    redirectTo: "image-content",
                    pathMatch: "full"
                },
                {
                    path: 'image-content',
                    component: ImageContentComponent
                },
                {
                    path: 'textual-content',
                    component: TextualContentComponent
                },
                {
                    path: 'large-content',
                    component: LargeContentComponent
                },
                {
                    path: 'sections',
                    component: SectionsComponent
                }
            ]

        /*{
 path: 'sample',
 component: SampleFeatureComponent,
 // loadComponent: () => import('./main/features/sample-feature/sample-feature.component').then(mod => mod.SampleFeatureComponent)
},
{
 path: 'admin',
 loadChildren: () => import('./admin/features/admin-feature/admin-feature.routes').then(mod => mod.routes)
},*/
        //loadChildren: () => import('./layout/layout/layout/layout.routes').then(mod => mod.routes)
    }
];
