# SharePoint custom callout for new item hero button

## What is this????

The main idea of this project was very simple. Look at this standard callout in SharePoint 2013 for a document library with enabled WOPI.

 ![](https://sergeisnitko.github.io/repos/spf-newitemcallout/01.png)

In addition, what do we have in lists? A hero button, that refers to a new form of default content type. However, what can we do if we have several content types linked to a list or even a folder? The only way is to use the new button in ribbon and not to use the hero button, because it is unpredictable.

Therefore, the main feature of this project is to make this callout with list of enabled content types for each folder in list or a library.


![](https://sergeisnitko.github.io/repos/spf-newitemcallout/02.png)

![](https://sergeisnitko.github.io/repos/spf-newitemcallout/03.png)

Next feature is solving the problem of rendering hero button if needs. In different versions of clienttemplates.js there is different check of it. Look at the function ShouldRenderHeroButton

```javascript
function ShouldRenderHeroButton(a){

varb=a.ListSchema;

return!Boolean(a.DisableHeroButton)&&(!b.IsDocLib||ListModule.Util.canUploadFile(a)||a.ListTemplateType==119||Boolean(a.NewWOPIDocumentEnabled))&&b.FolderRight_AddListItems!=null&&(b.Toolbar=="Freeform"||typeof window["heroButtonWebPart"+a.wpq]!="undefined"&&b.Toolbar=="Standard")

}
```

*Rendercontext.ListSchema.FolderRight_AddListItems* returns correct information about user permissions for current folder. Nevertheless, in some installations I can see this row:

*Rendercontext.ListSchema.ListRight_AddListItems*

It is really incorrect. User can have only permissions for adding items in folder, but not in list (in RootFolder). Therefore, we have to solve this problem to work the solution on every installation. How did we solve it? Sure, we could use FolderRight_AddListItems instructions, but we use JSOM to load enabled content types, so we decided to extend permissions check and it is based on *EffectiveBasePermissions* of folder item. Certainly, you can use *FolderRight_AddListItems* in your code against *EffectiveBasePermissions* check.

---
During using of the solution, it started to grow and today main functions of the solution are:
 * The user can see the hero button only if the user has permissions in current folder 
 * You can set the dependencies of content types for creation in settings javascript file
 * There is an option to show or use standard link if the content type to create is only one
 * There is an option to show a loading gif on the page instead of grid with data before callout data is loaded
 * There is an option that automatically reloads the page if the set of available content types updates. This option fixes the problem with available buttons in ribbon 
 * There is an option that updates current folders available content types by the *ContentTypes* param in options
 * The solution shows the extended buttons for creating office documents on document libraries if office web apps is available
 * The solution works in SharePoint Foundation 2013, SharePoint Server 2013, SharePoint Online, SharePoint Server 2016
 * The solution has a localization for different languages. By default there is ru-ru localization and en-us (witch is by default)
 * The solution can work with enabled minimal download strategy feature
 * The solution is easy to configure and use

## How can we use it?

You need to download the release file *spf.nic.applyto.js* and *spf.newitemcallout.js* and place it to the *SiteAssets* folder (SiteAssets, Style library, _catalogs/masterpage) and place it to a page with list webpart any handy way. I prefer to use *JSLink*, like this:

1. Edit the page

![](https://sergeisnitko.github.io/repos/spf-newitemcallout/04.png)

2. Edit the list view webpart

![](https://sergeisnitko.github.io/repos/spf-newitemcallout/05.png)

3. Set the link to *spf.nic.applyto.js* and *spf.newitemcallout.js* into the *JSLink* field like this:
*~sitecollection/_catalogs/masterpage/SPF/Modules/newitemcallout/spf.nic.applyto.js|~sitecollection/_catalogs/masterpage/SPF/Modules/newitemcallout/spf.newitemcallout.js*

![](https://sergeisnitko.github.io/repos/spf-newitemcallout/06.png)


## How can we use it with SPMeta2 (M2)?
The sources contains the SPMeta2 model for deploying  the js file in _catalogs/masterpage folder of the solution. You can use this model in your own solution or deploy with [this solution](https://github.com/sergeisnitko/sp-cmd-deploy)

## How can we use in my own solution?
You can download the sources and copy files and model to your own project, update settings of the solution and set the JSLink of the views by your own. But there is an easy way - use the [nuget](https://www.nuget.org/packages/sp-newitemcallout/) for your solution in Visual Studio.
```
Install-Package sp-newitemcallout
```
## License
The library is made available under the MIT license. http://en.m.wikipedia.org/wiki/MIT_License

## **Have a nice use of this solution!**

 
