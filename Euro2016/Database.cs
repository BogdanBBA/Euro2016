using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Euro2016
{
    /// <summary>
    /// Defines information related to user settings. Member property names are sufficiently explicit.
    /// </summary>
    public class Settings
    {
        public Team FavoriteTeam { get; set; }
        public bool ShowCountryNamesInNativeLanguage { get; set; }
        public bool ShowKnockoutStageOnStartup { get; set; }
        public bool SpamWithWinnerOnStartup { get; set; }
        public bool ShowFlagsOnMap { get; set; }

        /// <summary>Parses the settings from a XmlNode object.</summary>
        /// <param name="teams">the reference to the full list of teams participating in the competition</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string ReadSettings(XmlNode settingsNode, List<Team> teams)
        {
            try
            {
                try { this.FavoriteTeam = teams.First(t => t.Country.ID.Equals(settingsNode.SelectSingleNode("FavoriteTeam").Attributes["value"].Value)); }
                catch (Exception) { this.FavoriteTeam = teams.First(); }

                try { this.ShowCountryNamesInNativeLanguage = bool.Parse(settingsNode.SelectSingleNode("ShowCountryNamesInNativeLanguage").Attributes["value"].Value); }
                catch (Exception) { this.ShowCountryNamesInNativeLanguage = false; }

                try { this.ShowKnockoutStageOnStartup = bool.Parse(settingsNode.SelectSingleNode("ShowKnockoutStageOnStartup").Attributes["value"].Value); }
                catch (Exception) { this.ShowKnockoutStageOnStartup = true; }

                try { this.SpamWithWinnerOnStartup = bool.Parse(settingsNode.SelectSingleNode("SpamWithWinnerOnStartup").Attributes["value"].Value); }
                catch (Exception) { this.SpamWithWinnerOnStartup = true; }

                try { this.ShowFlagsOnMap = bool.Parse(settingsNode.SelectSingleNode("ShowFlagsOnMap").Attributes["value"].Value); }
                catch (Exception) { this.ShowFlagsOnMap = true; }

                return "";
            }
            catch (Exception E) { return E.ToString(); }
        }

        /// <summary>Generates a XmlNode object from the settings.</summary>
        /// <param name="doc">the XmlDocument object that will create the XML elements</param>
        /// <param name="nodeName">the name of the resulting XML node</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string ToXml(XmlDocument doc, string nodeName, out XmlNode resultNode)
        {
            try
            {
                XmlNode result = doc.CreateElement(nodeName), node;

                node = result.AppendChild(doc.CreateElement("FavoriteTeam"));
                node.AddAttribute(doc, "value", this.FavoriteTeam.Country.ID);

                node = result.AppendChild(doc.CreateElement("ShowCountryNamesInNativeLanguage"));
                node.AddAttribute(doc, "value", this.ShowCountryNamesInNativeLanguage.ToString());

                node = result.AppendChild(doc.CreateElement("ShowKnockoutStageOnStartup"));
                node.AddAttribute(doc, "value", this.ShowKnockoutStageOnStartup.ToString());

                node = result.AppendChild(doc.CreateElement("SpamWithWinnerOnStartup"));
                node.AddAttribute(doc, "value", this.SpamWithWinnerOnStartup.ToString());

                node = result.AppendChild(doc.CreateElement("ShowFlagsOnMap"));
                node.AddAttribute(doc, "value", this.ShowFlagsOnMap.ToString());

                resultNode = result;
                return "";
            }
            catch (Exception E) { resultNode = null; return E.ToString(); }
        }
    }

    /// <summary>
    /// Defines the top-level data for the application. Contains the centralized lists of various data types, read and write methods, and utility methods.
    /// </summary>
    public class Database
    {
        /// <summary>The identifier for any third-placed teams group in the team references of a match.</summary>
        public const string ThirdPlacedTeamsID = "T";

        /// <summary>Gets or privately sets the list of venues involved at Euro 2016.</summary>
        public ListOfIDObjects<Venue> Venues { get; private set; }
        /// <summary>Gets or privately sets the list of all countries used within the application.</summary>
        public ListOfIDObjects<Country> Countries { get; private set; }
        /// <summary>Gets or privately sets the list of clubs used within the application.</summary>
        public ListOfIDObjects<Club> Clubs { get; private set; }
        /// <summary>Gets or privately sets the list of players used within the application.</summary>
        public ListOfIDObjects<Player> Players { get; private set; }
        /// <summary>Gets or privately sets the list of teams involved at Euro 2016.</summary>
        public List<Team> Teams { get; private set; }
        /// <summary>Gets or privately sets the list of ranking groups of Euro 2016.</summary>
        public ListOfIDObjects<Group> Groups { get; private set; }
        /// <summary>Gets or privately sets the list of matches of Euro 2016.</summary>
        public ListOfIDObjects<Match> Matches { get; private set; }
        /// <summary>Gets or privately sets the user settings for the application.</summary>
        public Settings Settings { get; private set; }

        /// <summary>Constructs an empty Database object.</summary>
        public Database()
        {
            this.Venues = new ListOfIDObjects<Venue>();
            this.Countries = new ListOfIDObjects<Country>();
            this.Clubs = new ListOfIDObjects<Club>();
            this.Players = new ListOfIDObjects<Player>();
            this.Teams = new List<Team>();
            this.Groups = new ListOfIDObjects<Group>();
            this.Matches = new ListOfIDObjects<Match>();
            this.Settings = new Settings();
        }

        /// <summary>Loads a Database object using data from the database file.</summary>
        /// <param name="mainDatabaseFilePath">the path to the main part of the database file</param>
        /// <param name="playerDatabaseFilePath">the path to the player part of the database file</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string LoadDatabase(string mainDatabaseFilePath, string playerDatabaseFilePath)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mainDatabaseFilePath);

                XmlNodeList nodes = doc.SelectNodes("DATABASE/VENUES/Venue");
                foreach (XmlNode node in nodes)
                    this.Venues.Add(Venue.Parse(node));

                nodes = doc.SelectNodes("DATABASE/COUNTRIES/Country");
                foreach (XmlNode node in nodes)
                    this.Countries.Add(Country.Parse(node));

                nodes = doc.SelectNodes("DATABASE/TEAMS/Team");
                foreach (XmlNode node in nodes)
                    this.Teams.Add(Team.Parse(node, this.Countries));

                nodes = doc.SelectNodes("DATABASE/GROUPS/Group");
                foreach (XmlNode node in nodes)
                    this.Groups.Add(Group.Parse(node, this.Teams));

                nodes = doc.SelectNodes("DATABASE/MATCHES/Match");
                foreach (XmlNode node in nodes)
                    this.Matches.Add(Match.Parse(node, this));

                this.Settings.ReadSettings(doc.SelectSingleNode("DATABASE/SETTINGS"), this.Teams);

                doc = new XmlDocument();
                doc.Load(playerDatabaseFilePath);

                nodes = doc.SelectNodes("PLAYER_DATABASE/CLUBS/Club");
                foreach (XmlNode node in nodes)
                    this.Clubs.Add(Club.Parse(node, this.Countries));

                nodes = doc.SelectNodes("PLAYER_DATABASE/PLAYERS/Player");
                foreach (XmlNode node in nodes)
                {
                    Player player = Player.Parse(node, this.Teams, this.Clubs);
                    player.Nationality.Players.Add(player);
                    this.Players.Add(player);
                }

                this.Calculate(true, true, true);

                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Saves the database to file.</summary>
        /// <param name="mainDatabaseFilePath">the path to the main part of the database file</param>
        /// <param name="playerDatabaseFilePath">the path to the player part of the database file. If null (or not passed), this part will not be written</param>
        /// <returns>an empty string if execution ended successfully, or the error description otherwise</returns>
        public string SaveDatabase(string mainDatabaseFilePath, string playerDatabaseFilePath = null)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.AppendChild(doc.CreateElement("DATABASE"));
                root.AddAttribute(doc, "lastSaved", DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss"));

                XmlNode node = root.AppendChild(doc.CreateElement("VENUES"));
                foreach (Venue venue in this.Venues)
                    node.AppendChild(venue.ToXml(doc, "Venue"));

                node = root.AppendChild(doc.CreateElement("COUNTRIES"));
                foreach (Country country in this.Countries)
                    node.AppendChild(country.ToXml(doc, "Country"));

                node = root.AppendChild(doc.CreateElement("TEAMS"));
                foreach (Team team in this.Teams)
                    node.AppendChild(team.ToXml(doc, "Team"));

                node = root.AppendChild(doc.CreateElement("GROUPS"));
                foreach (Group group in this.Groups)
                    node.AppendChild(group.ToXml(doc, "Group"));

                node = root.AppendChild(doc.CreateElement("MATCHES"));
                foreach (Match match in this.Matches)
                    node.AppendChild(match.ToXml(doc, "Match"));

                XmlNode settingsNode;
                this.Settings.ToXml(doc, "SETTINGS", out settingsNode);
                root.AppendChild(settingsNode);

                doc.Save(mainDatabaseFilePath);

                if (playerDatabaseFilePath != null)
                {
                    doc = new XmlDocument();
                    root = doc.AppendChild(doc.CreateElement("PLAYER_DATABASE"));
                    root.AddAttribute(doc, "lastSaved", DateTime.Now.ToString("dddd, d MMMM yyyy, HH:mm:ss"));

                    node = root.AppendChild(doc.CreateElement("CLUBS"));
                    foreach (Club club in this.Clubs)
                        node.AppendChild(club.ToXml(doc, "Club"));

                    node = root.AppendChild(doc.CreateElement("PLAYERS"));
                    foreach (Player player in this.Players)
                        node.AppendChild(player.ToXml(doc, "Player"));

                    doc.Save(playerDatabaseFilePath);
                }

                return "";
            }
            catch (Exception E)
            { return E.ToString(); }
        }

        /// <summary>Utility method. Calls the respective 'calculate' methods for the parameter values that are set to true</summary>
        public void Calculate(bool groupMatchTeams, bool groups, bool knockoutMatches)
        {
            if (groupMatchTeams)
                this.CalculateGroupMatchTeams();
            if (groups)
                this.CalculateGroups();
            if (knockoutMatches)
                this.CalculateKnockoutMatches();
        }

        /// <summary>Calculates the group matches' team references. Should normally only be called once during execution.</summary>
        public void CalculateGroupMatchTeams()
        {
            foreach (Match match in this.Matches)
                if (match.IsGroupMatch)
                {
                    match.Teams.Home = this.Teams.First(t => t.Country.ID.Equals(match.TeamReferences.Home));
                    match.Teams.Away = this.Teams.First(t => t.Country.ID.Equals(match.TeamReferences.Away));
                }
        }

        /// <summary>Calculates the group rankings (resets rankings, adds all valid match results, then sorts).</summary>
        public void CalculateGroups()
        {
            foreach (Group group in this.Groups)
            {
                foreach (TableLine line in group.TableLines)
                {
                    line.Reset();
                    foreach (Match match in this.Matches.GetMatchesBy(group))
                        line.AddGroupMatchResult(match);
                }
                group.SortTableLines(false, this.Matches);
            }
        }

        /// <summary>Parses a team reference (in the format 'refGroup:refTeam'; for example, "B:1" or "T:A/C/D") and, if valid, returns the correctly corresponding Team object, or null otherwise</summary>
        private Team ParseTeamReference(string reference)
        {
            if (reference.Contains(':')) // table line reference
            {
                string refGroup = reference.Split(':')[0], refTeam = reference.Split(':')[1];

                if (!refGroup.Equals(Database.ThirdPlacedTeamsID)) // normal group
                {
                    Group group = this.Groups.GetItemByID(refGroup);
                    if (group != null && group.AllMatchesPlayed)
                        return group.TableLines[Int32.Parse(refTeam) - 1].Team;
                    return null;
                }
                else // third-place group (also, certainly an eight-final)
                {
                    Group tempGroup = new Group("temp", refTeam, new List<TableLine>());
                    foreach (string groupID in refTeam.Split('/'))
                    {
                        Group group = this.Groups.GetItemByID(groupID);
                        if (!group.AllMatchesPlayed)
                            return null;
                        tempGroup.TableLines.Add(new TableLine(group.TableLines[2]));
                    }
                    tempGroup.SortTableLines(true, this.Matches);
                    for (int index = 0; index < tempGroup.TableLines.Count; index++)
                        if (this.Matches.GetMatchesBy("KO:8").GetMatchesBy(tempGroup.TableLines[index].Team).Count == 0)
                            return tempGroup.TableLines[index].Team;
                    return null;
                }
            }
            else // match winner reference
            {
                Match match = this.Matches.GetItemByID(reference);
                if (match.Teams.Home != null & match.Teams.Away != null && match.Scoreboard.Played)
                    return match.Scoreboard.FullScore.HomeWin ? match.Teams.Home : match.Teams.Away;
                return null;
            }
        }

        /// <summary>Calculates the knockout matches' team references.</summary>
        public void CalculateKnockoutMatches()
        {
            ListOfIDObjects<Match> matches = this.Matches.GetMatchesBy("KO");
            foreach (Match match in matches)
            {
                match.Teams.Home = null;
                match.Teams.Away = null;
            }
            foreach (Match match in matches)
            {
                match.Teams.Home = this.ParseTeamReference(match.TeamReferences.Home);
                match.Teams.Away = this.ParseTeamReference(match.TeamReferences.Away);
            }
        }

        /// <summary>Determines the best result so far in the competition of the given team. More specifically, it returns the category of the last (chronologically-sorted) match involving the given team.</summary>
        public string TournamentResultOfTeam(Team team)
        {
            return this.Matches.GetMatchesBy(team).Last().Category;
        }
    }
}
