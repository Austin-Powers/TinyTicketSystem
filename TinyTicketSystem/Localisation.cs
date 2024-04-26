using System.Resources;

namespace TinyTicketSystem
{
    /// <summary>
    /// Simplifies acces to the localisation resources.
    /// </summary>
    public class Localisation
    {
        private static readonly string ID1 = "{1}";
        private static readonly string ID2 = "{2}";

        // All users of this class should be in the same assembly
        private readonly ResourceManager _resourceManager = new ResourceManager("TinyTicketSystem.TinyTicketSystem", typeof(Localisation).Assembly);

        #region FilterLocalisation
        public class FilterLocalisation
        {
            public string StatusAll { get; private set; }
            public string StatusBlocked { get; private set; }
            public string StatusClosed { get; private set; }
            public string StatusOpen { get; private set; }
            public string StatusOpenOrBlocked { get; private set; }
            public string TitleEmpty { get; private set; }

            public FilterLocalisation(ResourceManager rm)
            {
                StatusAll = rm.GetString("filter_status_all");
                StatusBlocked = rm.GetString("filter_status_blocked");
                StatusClosed = rm.GetString("filter_status_closed");
                StatusOpen = rm.GetString("filter_status_open");
                StatusOpenOrBlocked = rm.GetString("filter_status_open_or_blocked");
                TitleEmpty = rm.GetString("filter_title_empty");
            }
        }
        public FilterLocalisation Filter { get; private set; }
        #endregion
        #region MainLocalisation
        public class MainLocalisation
        {
            private string _deleteConfirm;
            private string _deleteError;
            private string _dirError;
            private string _editClose;
            private string _editOpen;
            private string _loadFailure;
            private string _loadSuccess;
            private string _newError;

            public string DeleteConfirm(object ticket) { return _deleteConfirm.Replace(ID1, ticket.ToString()); }
            public string DeleteError(string errorMesssage) { return _deleteError.Replace(ID1, errorMesssage); }
            public string DeleteQuestion {  get; private set; }
            public string DirError(string errorMessage) { return _dirError.Replace(ID1, errorMessage); }
            public string EditClose(object ticket) { return _editClose.Replace(ID1, ticket.ToString()); }
            public string EditOpen(object ticket) { return _editOpen.Replace(ID1, ticket.ToString()); }
            public string File {  get; private set; }
            public string FileDeleteTicket {  get; private set; }
            public string FileNewTicket {  get; private set; }
            public string FileRefresh {  get; private set; }
            public string FileSetTicketDir {  get; private set; }
            public string LoadFailure(string dir,  string errorMessage) { return _loadFailure.Replace(ID1, dir).Replace(ID2, errorMessage); }
            public string LoadSuccess(string dir) { return _loadSuccess.Replace(ID1, dir); }
            public string NewTicketCreated { get; private set; }
            public string NewTicketDiscarded { get; private set; }
            public string NewTicketError(string errorMessage) { return _newError.Replace(ID1, errorMessage); }
            public string TableBlocked { get; private set; }
            public string TableClosed { get; private set; }
            public string TableID { get; private set; }
            public string TableLastChanged { get; private set; }
            public string TableOpen { get; private set; }
            public string TableStatus { get; private set; }
            public string TableTags { get; private set; }
            public string TableTitle { get; private set; }

            public MainLocalisation(ResourceManager rm)
            {
                _deleteConfirm = rm.GetString("main_delete_confirm");
                _deleteError = rm.GetString("main_delete_error");
                _dirError = rm.GetString("main_dir_error");
                _editClose = rm.GetString("main_edit_close");
                _editOpen = rm.GetString("main_edit_open");
                _loadFailure = rm.GetString("main_load_failure");
                _loadSuccess = rm.GetString("main_load_success");
                _newError = rm.GetString("main_new_error");

                DeleteQuestion = rm.GetString("main_delete_question");
                File = rm.GetString("main_file");
                FileDeleteTicket = rm.GetString("main_file_delete_ticket");
                FileNewTicket = rm.GetString("main_file_new_ticket");
                FileRefresh = rm.GetString("main_file_refresh");
                FileSetTicketDir = rm.GetString("main_file_set_ticket_dir");
                NewTicketCreated = rm.GetString("main_new_created");
                NewTicketDiscarded = rm.GetString("main_new_discarded");
                TableBlocked = rm.GetString("main_table_blocked");
                TableClosed = rm.GetString("main_table_closed");
                TableID = rm.GetString("main_table_id");
                TableLastChanged = rm.GetString("main_table_last_changed");
                TableOpen = rm.GetString("main_table_open");
                TableStatus = rm.GetString("main_table_status");
                TableTags = rm.GetString("main_table_tags");
                TableTitle = rm.GetString("main_table_title");
            }
        }
        public MainLocalisation Main { get; private set; }
        #endregion
        #region SelectorLocalisation
        public class SelectorLocalisation
        {
            public string Add { get; private set; }
            public string Title { get; private set; }

            public SelectorLocalisation(ResourceManager rm)
            {
                Add = rm.GetString("selector_add");
                Title = rm.GetString("selector_title");
            }
        }
        public SelectorLocalisation Selector { get; private set; }
        #endregion
        #region TicketLocalisation
        public class TicketLocalisation
        {
            public string AddTicket { get; private set; }
            public string DetailsEmpty { get; private set; }
            public string NewTicket { get; private set; }
            public string Remove { get; private set; }
            public string StatusBlocked { get; private set; }
            public string StatusClose { get; private set; }
            public string StatusReopen { get; private set; }
            public string TagEmpty { get; private set; }
            public string TitleEmpty { get; private set; }

            public TicketLocalisation(ResourceManager rm)
            {
                AddTicket = rm.GetString("ticket_add_ticket");
                DetailsEmpty = rm.GetString("ticket_details_empty");
                NewTicket = rm.GetString("ticket_new_ticket");
                Remove = rm.GetString("ticket_remove");
                StatusBlocked = rm.GetString("ticket_status_blocked");
                StatusClose = rm.GetString("ticket_status_close");
                StatusReopen = rm.GetString("ticket_status_reopen");
                TagEmpty = rm.GetString("ticket_tag_empty");
                TitleEmpty = rm.GetString("ticket_title_empty");
            }
        }
        public TicketLocalisation TicketLoc { get; private set; }
        #endregion

        public Localisation()
        {
            var rm = new ResourceManager("TinyTicketSystem.TinyTicketSystem", typeof(Localisation).Assembly);
            Filter = new FilterLocalisation(rm);
            Main = new MainLocalisation(rm);
            Selector = new SelectorLocalisation(rm);
            TicketLoc = new TicketLocalisation(rm);
        }

        /// <summary>
        /// Gets the localised string for the given name.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <returns>The localised string.</returns>
        public string Get(string name)
        {
            return _resourceManager.GetString(name);
        }

        /// <summary>
        /// Gets the localised string from the given name and insert the given argument.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <param name="arg">The argument to put into the placeholder.</param>
        /// <returns>The localised string with the inserted argument.</returns>
        public string Get(string name, object arg)
        {
            return Get(name).Replace("{1}", arg.ToString());
        }

        /// <summary>
        /// Gets the localised string from the given name and insert the given argument.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <param name="arg0">The argument to put into the first placeholder.</param>
        /// <param name="arg1">The argument to put into the second placeholder.</param>
        /// <returns>The localised string with the inserted argument.</returns>
        public string Get(string name, object arg0, object arg1)
        {
            return Get(name).Replace("{1}", arg0.ToString()).Replace("{2}", arg1.ToString());
        }
    }
}
