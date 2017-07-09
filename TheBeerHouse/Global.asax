<%@ Application Language="C#" %>

<script runat="server">

   void Application_Start(Object sender, EventArgs e) {
      // Code that runs on application startup

   }

   void Application_End(Object sender, EventArgs e) {
      //  Code that runs on application shutdown

   }
      
   void Application_Error(Object sender, EventArgs e)
   { 

   }

   void Session_Start(Object sender, EventArgs e) {
      // Code that runs when a new session is started

   }

   void Session_End(Object sender, EventArgs e) {
      // Code that runs when a session ends. 
      // Note: The Session_End event is raised only when the sessionstate mode
      // is set to InProc in the Web.config file. If session mode is set to StateServer 
      // or SQLServer, the event is not raised.

   }

   void Profile_MigrateAnonymous(object sender, ProfileMigrateEventArgs e)
   {
      // get a reference to the previously anonymous user's profile
      ProfileCommon anonProfile = this.Profile.GetProfile(e.AnonymousID);
      // if set, copy its Theme and ShoppingCart to the current user's profile
      if (anonProfile.ShoppingCart.Items.Count > 0)
         this.Profile.ShoppingCart = anonProfile.ShoppingCart;
      if (!string.IsNullOrEmpty(anonProfile.Preferences.Theme))
         this.Profile.Preferences.Theme = anonProfile.Preferences.Theme;
      // delete the anonymous profile
      ProfileManager.DeleteProfile(e.AnonymousID);
      AnonymousIdentificationModule.ClearAnonymousIdentifier();
   }

   /*
   void Application_PostAcquireRequestState(Object sender, EventArgs e)
   {
      System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(
         this.Profile.Preferences.Culture);
      System.Threading.Thread.CurrentThread.CurrentCulture = culture;
      System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
   }
   */
       
</script>
