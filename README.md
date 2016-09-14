#SharePoint 2013 custom callout for new item hero button

##What is this?

The main idea of this project was very simple. Look at this standard callout in SharePoint 2013 for a document library with enabled WOPI.

 

In addition, what do we have in lists? A hero button, that refers to a new form of default content type. However, what can we do if we have several content types linked to a list or even a folder? The only way is to use the new button in ribbon and not to use the hero button, because it is unpredictable.

Therefore, the main feature of this project is to make this callout with list of enabled content types for each folder in list or a library.




Next feature is solving the problem of rendering hero button if needs. In different versions of clienttemplates.js there is different check of it. Look at the function ShouldRenderHeroButton

function ShouldRenderHeroButton(a){

varb=a.ListSchema;

return!Boolean(a.DisableHeroButton)&&(!b.IsDocLib||ListModule.Util.canUploadFile(a)||a.ListTemplateType==119||Boolean(a.NewWOPIDocumentEnabled))&&b.FolderRight_AddListItems!=null&&(b.Toolbar=="Freeform"||typeof window["heroButtonWebPart"+a.wpq]!="undefined"&&b.Toolbar=="Standard")

}

Rendercontext.ListSchema.FolderRight_AddListItems returns correct information about user permissions for current folder. Nevertheless, in some installations I can see this row:

Rendercontext.ListSchema.ListRight_AddListItems

It is really incorrect. User can have only permissions for adding items in folder, but not in list (in RootFolder). Therefore, we have to solve this problem to work the solution on every installation. How did we solve it? Sure, we could use FolderRight_AddListItems instructions, but we use JSOM to load enabled content types, so we decided to extend permissions check and it is based on EffectiveBasePermissions of folder item. Certainly, you can use FolderRight_AddListItems in your code against EffectiveBasePermissions check.

##How can we use it?

You need to place the cib.newitemcallout.js file to a SiteAssets folder and place it to a page with list webpart any handy way. I prefer to use JSLink, like this:

1. Edit the page



2. Edit the list view webpart



3. Set the link to cib.newitemcallout.js into the JSLink field



Have a nice use of this solution!

 
