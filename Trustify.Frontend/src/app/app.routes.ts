import { Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout/layout/layout.component';
import { ImageContentComponent } from './main/features/image-content/image-content/image-content.component';
import { TextualContentComponent } from './main/features/textual-content/textual-content/textual-content.component';
import { SectionsComponent } from './main/features/sections/sections/sections.component';
import { ImageGeneratorComponent } from './main/features/europeana/image-generator/image-generator.component';
import { authGuard } from './main/core/auth/auth.guard';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        canActivate: [authGuard],
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
                    path: 'sections',
                    component: SectionsComponent
                },
                {
                    path: 'image-generator',
                    component: ImageGeneratorComponent
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
