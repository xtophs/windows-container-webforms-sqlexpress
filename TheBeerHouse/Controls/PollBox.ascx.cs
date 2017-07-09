using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MB.TheBeerHouse;
using MB.TheBeerHouse.UI;
using MB.TheBeerHouse.BLL.Polls;

namespace MB.TheBeerHouse.UI.Controls
{
   public partial class PollBox : BaseWebPart
   {
      public PollBox()
      {
         this.Title = "Poll Box";
      }

      [Personalizable(PersonalizationScope.Shared),
      WebBrowsable,
      WebDisplayName("Show Archive Link"),
      WebDescription("Specifies whether the link to the archive page is displayed")]
      public bool ShowArchiveLink
      {
         get { return lnkArchive.Visible; }
         set { lnkArchive.Visible = value; }
      }

      [Personalizable(PersonalizationScope.Shared),
      WebBrowsable,
      WebDisplayName("Show Header"),
      WebDescription("Specifies whether the header is displayed")]
      public bool ShowHeader
      {
         get { return panHeader.Visible; }
         set { panHeader.Visible = value; }
      }

      [Personalizable(PersonalizationScope.Shared),
      WebBrowsable,
      WebDisplayName("Header Text"),
      WebDescription("The text of the header")]
      public string HeaderText
      {
         get { return lblHeader.Text; }
         set { lblHeader.Text = value; }
      }      

      private int _pollID = -1;
      [Personalizable(PersonalizationScope.Shared),
      WebBrowsable,
      WebDisplayName("Poll ID"),
      WebDescription("The ID of the poll to show")]
      public int PollID
      {
         get { return _pollID; }
         set { _pollID = value; }
      }

      public bool ShowQuestion
      {
         get { return lblQuestion.Visible; }
         set { lblQuestion.Visible = value; }
      }

      protected void Page_Init(object sender, EventArgs e)
      {
         this.Page.RegisterRequiresControlState(this);
      }

      protected override void LoadControlState(object savedState)
      {
         object[] ctlState = (object[])savedState;
         base.LoadControlState(ctlState[0]);
         this.PollID = (int)ctlState[1];
      }

      protected override object SaveControlState()
      {
         object[] ctlState = new object[2];
         ctlState[0] = base.SaveControlState();
         ctlState[1] = this.PollID;
         return ctlState;
      }

      protected void Page_Load(object sender, EventArgs e)
      {
         if (!this.IsPostBack)
            DoBinding();
      }

      public override void DataBind()
      {
         // the call to the base DataBind makes a call to OnDataBinding,
         // which parses and evaluates the control's binding expressions, i.e. the PollID prop
         base.DataBind();
         // with the PollID set, do the actual binding
         DoBinding();
      }

      protected void DoBinding()
      {
         panResults.Visible = false;
         panVote.Visible = false;
         int pollID = (this.PollID == -1 ? Poll.CurrentPollID : this.PollID);

         if (pollID > -1)
         {
            Poll poll = Poll.GetPollByID(pollID);
            if (poll != null)
            {
               lblQuestion.Text = poll.QuestionText;
               lblTotalVotes.Text = poll.Votes.ToString();
               valRequireOption.ValidationGroup += poll.ID.ToString();
               btnVote.ValidationGroup = valRequireOption.ValidationGroup;
               optlOptions.DataSource = poll.Options;
               optlOptions.DataBind();
               rptOptions.DataSource = poll.Options;
               rptOptions.DataBind();
               // if the user has already voted (there's a cookie with its vote's ID),
               // or if the poll is archved, show the results, otherwise show the radio buttons and the Vote button            
               if (poll.IsArchived || GetUserVote(pollID) > 0)
                  panResults.Visible = true;
               else
                  panVote.Visible = true;
            }
         }
      }

      protected void btnVote_Click(object sender, EventArgs e)
      {
         int pollID = (this.PollID == -1 ? Poll.CurrentPollID : this.PollID);

         // check that the user has not already voted for this poll
         int userVote = GetUserVote(pollID);
         if (userVote == 0)
         {
            // post the vote and then create a cookie to remember this user's vote
            userVote = Convert.ToInt32(optlOptions.SelectedValue);
            Poll.VoteOption(pollID, userVote);
            // hide the panel with the radio buttons, and show the results
            DoBinding();
            panVote.Visible = false;
            panResults.Visible = true;

            DateTime expireDate = DateTime.Now.AddDays(Globals.Settings.Polls.VotingLockInterval);
            string key = "Vote_Poll" + pollID.ToString();

            // save the result to the cookie
            if (Globals.Settings.Polls.VotingLockByCookie)
            {
               HttpCookie cookie = new HttpCookie(key, userVote.ToString());
               cookie.Expires = expireDate;
               this.Response.Cookies.Add(cookie);
            }

            // save the vote also to the cache, for better security 
            // during the current session (i.e. the vote will be recorded even
            // if cookie are disable on the client
            if (Globals.Settings.Polls.VotingLockByIP)
            {
               Cache.Insert(
                  this.Request.UserHostAddress.ToString() + "_" + key,
                  userVote);
            }
         }
      }

      protected int GetUserVote(int pollID)
      {
         string key = "Vote_Poll" + pollID.ToString();
         string key2 = this.Request.UserHostAddress.ToString() + "_" + key;

         // check if the vote is in the cache
         if (Globals.Settings.Polls.VotingLockByIP && Cache[key2] != null)
            return (int)Cache[key2];

         // if the vote is not in cache, check if there's a client-side cookie
         if (Globals.Settings.Polls.VotingLockByCookie)
         {
            HttpCookie cookie = this.Request.Cookies[key];
            if (cookie != null)
               return int.Parse(cookie.Value);
         }

         return 0;
      }

      protected int GetFixedPercentage(object val)
      {
         int percentage = Convert.ToInt32(val);
         if (percentage == 100)
            percentage = 98;
         return percentage;
      }
   }
}