﻿using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.TeamServices.Samples.Client.WorkItemTracking
{
    [ClientSample(WitConstants.WorkItemTrackingWebConstants.RestAreaName, WitConstants.WorkItemTrackingRestResources.Queries)]
    public class QueriesSample : ClientSample
    {
        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueries()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            
            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();
            
            try
            {
                List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.None, 1).Result;

                if (queries.Count == 0)
                {
                    Console.WriteLine("No queries found");
                }
                else
                {
                    Console.WriteLine("Queries:");

                    foreach (var query in queries)
                    {
                        Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                    }
                }                

                return queries;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting queries: " + ex.InnerException.Message);

                return null;
            }         
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderByFolderPath()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            string queryName = "Shared Queries/Current Iteration";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(project, queryName, null, 2).Result;

                if (query == null)
                {
                    Console.WriteLine("No queries found for path '{0}'", queryName);
                }
                else
                {
                    Console.WriteLine("Queries:");

                    foreach (var item in query.Children)
                    {
                        Console.WriteLine("{0}", item.Name);
                        Console.WriteLine("  {0}", item.Id);
                        Console.WriteLine("  {0}", item.Path);
                        Console.WriteLine();
                    }
                }

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query or folder: " + ex.InnerException.Message);

                return null;
            }
            
        }

        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueriesAndFoldersWithOptions()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {  
                List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.All, 1).Result;

                if (queries.Count == 0)
                {
                    Console.WriteLine("No queries found");
                }
                else
                {
                    Console.WriteLine("Queries:");

                    foreach (var query in queries)
                    {
                        Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                    }
                }

                return queries;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query: " + ex.InnerException.Message);

                return null;
            }
        }

        [ClientSampleMethod]
        public List<QueryHierarchyItem> GetListOfQueriesAndFoldersIncludeDeleted()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                List<QueryHierarchyItem> queries = workItemTrackingClient.GetQueriesAsync(projectId, QueryExpand.None, 1, true).Result;
                
                if (queries.Count == 0)
                {
                    Console.WriteLine("No queries found");
                }
                else
                {
                    Console.WriteLine("Queries:");

                    foreach (var query in queries)
                    {
                        Console.WriteLine("  {0} - {1}", query.Name, query.Path);
                    }
                }

                return queries;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query: " + ex.InnerException.Message);

                return null;
            }
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderById()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "a2108d31-086c-4fb0-afda-097e4cc46df4"; //assigned to me query

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {            
                QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryId).Result;

                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }        
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query: " + ex.InnerException.Message);

                return null;
            }           
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetQueryOrFolderByName()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryName = "Shared Queries/Current Iteration/Active Bugs";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryName).Result;

                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query: " + ex.InnerException.Message);

                return null;
            }           
        }

        [ClientSampleMethod]
        public QueryHierarchyItem GetDeletedQueryOrFolderById()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.GetQueryAsync(projectId, queryId, null, 1, true).Result;

                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error getting query: " + ex.InnerException.Message);

                return null;
            }           
        }

        [ClientSampleMethod]
        public QueryHierarchyItem CreateQuery()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryPath = "My Queries";

            QueryHierarchyItem postedQuery = new QueryHierarchyItem()
            {
                Name = "Sample Query",   
                Wiql = "Select [System.Id], [System.Title], [System.State] From WorkItems Where [System.WorkItemType] = 'Bug' order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.CreateQueryAsync(postedQuery, projectId, queryPath).Result;

                // Save the ID of the newly created query for use in later samples
                this.Context.SetValue<Guid>("$sampleQueryId", query.Id);
                
                Console.WriteLine("Query Successfully Created");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException.Message.Contains("TF237018"))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Error creating query: Query name in specified path already exists");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return null;
            }         
        }

        [ClientSampleMethod]
        public QueryHierarchyItem CreateFolder()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string queryPath = "Shared Queries";

            QueryHierarchyItem postedQuery = new QueryHierarchyItem()
            {
                Name = "Sample Folder",
                IsFolder = true
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.CreateQueryAsync(postedQuery, projectId, queryPath).Result;

                Console.WriteLine("Folder Successfully Created");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error creating folder: " + ex.InnerException.Message);
                return null;
            }          
        }

        [ClientSampleMethod]
        public QueryHierarchyItem UpdateQuery()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid queryId = this.Context.GetValue<Guid>("$sampleQueryId");

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {               
                Wiql = "Select [System.Id], [System.Title], [System.State] From WorkItems Where [System.WorkItemType] = 'Bug' AND [System.State] = 'Active' order by [Microsoft.VSTS.Common.Priority] asc, [System.CreatedDate] desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId.ToString()).Result;

                Console.WriteLine("Query updated successfully");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error updating query: " + ex.InnerException.Message);
                return null;
            }                
        }

        [ClientSampleMethod]
        public QueryHierarchyItem RenameQueryOrFolder()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid queryId = this.Context.GetValue<Guid>("$sampleQueryId");

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {
                Name = "Renamed Sample Query"  
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {           
                QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId.ToString()).Result;

                Console.WriteLine("Query renamed successfully");
                Console.WriteLine("Id:         {0}", query.Id);
                Console.WriteLine("Name:       {0}", query.Name);
                Console.WriteLine("Path:       {0}", query.Path);

                return query;
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error updating query: " + ex.InnerException.Message);
                return null;
            }            
        }

        //[ClientSampleMethod]
        //public QueryHierarchyItem MoveQueryOrFolder()
        //{
        //    Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
        //    string queryId = "2614c4de-be48-4735-9fdc-9656f55c495f";

        //    QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
        //    {
        //        Id = new Guid("8a8c8212-15ca-41ed-97aa-1d6fbfbcd581") //where you want to move the queryId too
        //    };

        //    VssConnection connection = Context.Connection;
        //    WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

        //    QueryHierarchyItem query = workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId).Result;

        //    if (query == null)
        //    {
        //        Console.WriteLine("Error moving query");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Query/folder moved successfully");
        //        Console.WriteLine("Id:         {0}", query.Id);
        //        Console.WriteLine("Name:       {0}", query.Name);
        //        Console.WriteLine("Path:       {0}", query.Path);
        //    }

        //    return query;
        //}

        [ClientSampleMethod]
        public void DeleteQueryOrFolderById()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid queryId = this.Context.GetValue<Guid>("$sampleQueryId");

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            workItemTrackingClient.DeleteQueryAsync(projectId, queryId.ToString());

            Console.WriteLine("Query/folder deleted");                
        }

        [ClientSampleMethod]
        public void DeleteQueryOrFolderByPath()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            string path = "My Queries/Sample";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();
            
            workItemTrackingClient.DeleteQueryAsync(projectId, path);

            Console.WriteLine("Query/folder deleted");
        }

        [ClientSampleMethod]
        public void UnDeleteQueryOrFolder()
        {
            Guid projectId = ClientSampleHelpers.FindAnyProject(this.Context).Id;
            Guid queryId = this.Context.GetValue<Guid>("$sampleQueryId");

            QueryHierarchyItem queryUpdate = new QueryHierarchyItem()
            {
                IsDeleted = false
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            workItemTrackingClient.UpdateQueryAsync(queryUpdate, projectId, queryId.ToString(), true);

            Console.WriteLine("Query/folder deleted");
        }

        [ClientSampleMethod]
        public WorkItemQueryResult ExecuteQuery()
        {
            Guid queryId = Guid.Parse("a2108d31-086c-4fb0-afda-097e4cc46df4"); // assigned to me

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            try
            {
                WorkItemQueryResult queryResult = workItemTrackingClient.QueryByIdAsync(queryId).Result;

                return queryResult;
            }
            catch (AggregateException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.InnerException.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return null;
            }           
        }

        [ClientSampleMethod]
        public WorkItemQueryResult ExecuteByWiql()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            Wiql wiql = new Wiql()
            {
                Query = "Select ID, Title from Issue where (State = 'Active') order by Title"
            };            

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByWiqlAsync(wiql, project).Result;

            return queryResult;
        }

        [ClientSampleMethod]
        public IEnumerable<WorkItem> GetWorkItemsFromQuery()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;
            string queryName = "Shared Queries/Current Sprint";

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            QueryHierarchyItem queryItem;

            try
            {
                // get the query object based on the query name and project
                queryItem = workItemTrackingClient.GetQueryAsync(project, queryName).Result;
            }
            catch (Exception ex)
            {
                // query was likely not found
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.InnerException.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return null;
            }

            // now we have the query, so let'ss execute it and get the results
            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByIdAsync(queryItem.Id).Result;

            if (queryResult.WorkItems.Count() == 0)
            {
                return new List<WorkItem>();
            }
            else
            {
                // need to get the list of our work item id's and put them into an array
                int[] workItemIds = queryResult.WorkItems.Select<WorkItemReference, int>(wif => { return wif.Id; }).ToArray();

                // build a list of the fields we want to see
                string[] fields = new []
                    {
                        "System.Id",
                        "System.Title",
                        "System.State"
                    };

                IEnumerable<WorkItem> workItems = workItemTrackingClient.GetWorkItemsAsync(workItemIds, fields, queryResult.AsOf).Result;

                return workItems;
            }
        }

        public IEnumerable<WorkItem> GetWorkItemsFromWiql()
        {
            string project = ClientSampleHelpers.FindAnyProject(this.Context).Name;

            // create a query to get your list of work items needed
            Wiql wiql = new Wiql()
            {
                Query = "Select [State], [Title] " +
                        "From WorkItems " +
                        "Where [Work Item Type] = 'Bug' " +
                        "And [System.TeamProject] = '" + project + "' " +
                        "And [System.State] = 'New' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            VssConnection connection = Context.Connection;
            WorkItemTrackingHttpClient workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();

            // execute the query
            WorkItemQueryResult queryResult = workItemTrackingClient.QueryByWiqlAsync(wiql).Result;

            // check to make sure we have some results
            if (queryResult.WorkItems.Count() == 0)
            {
                return new List<WorkItem>();
            }
            else
            {
                // need to get the list of our work item id's and put them into an array
                int[] workItemIds = queryResult.WorkItems.Select<WorkItemReference, int>(wif => { return wif.Id; }).ToArray();

                // build a list of the fields we want to see
                string[] fields = new []
                    {
                        "System.Id",
                        "System.Title",
                        "System.State"
                    };

                IEnumerable<WorkItem> workItems = workItemTrackingClient.GetWorkItemsAsync(
                    workItemIds, 
                    fields, 
                    queryResult.AsOf).Result;

                return workItems;
            }
        }

    }
}
