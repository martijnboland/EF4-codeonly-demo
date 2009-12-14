<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        This is a demo application to see how we can fit ASP.NET MVC 2 and Entity Framework 4 together nicely.
		By no means this is intended as a 'best practises' example. It's just a spike to test out various 
		new technologies.<br />
		The application is a very simple course registration system where you can register courses with their schedules and maintain teachers. 		
    </p>
	<p>
		Technologies used are:
	</p>
	<ul>
		<li>Entity Framework 4 with POCO entity objects and code-only configuration (no edmx)</li>
		<li>ASP.NET 2 MVC for the UI</li>
		<li>Data access abstracted via repository interfaces</li>
		<li>Validation with Data Annotations</li>
		<li>Castle Windsor IoC container to wire the various components together</li>
	</ul>
</asp:Content>
