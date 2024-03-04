import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { authGuard } from './_guards/auth.guard';
import { ErrorHandlerComponent } from './errors/error-handler.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'error', component: ErrorHandlerComponent },
    { path: 'not-found', component: NotFoundComponent },
    { path: 'server-error', component: ServerErrorComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [authGuard],
        children: [
            { path: 'members', component: MemberListComponent },
            { path: 'members/:username', component: MemberDetailComponent },
            { path: 'member/edit', component: MemberEditComponent, canDeactivate:[preventUnsavedChangesGuard] },
            { path: 'messages', component: MessagesComponent },
            { path: 'lists', component: ListsComponent },
            { path: '**', component: HomeComponent, pathMatch: 'full' }
        ]
    },
    { path: '**', component: NotFoundComponent, pathMatch: 'full' }
];
