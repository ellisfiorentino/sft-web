using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcSiteMapProvider;
using sft.Models;

namespace sft.Helpers
{
  public class NewsDynaicNodeProvider : DynamicNodeProviderBase 
  {
    public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
    {
      using (var db = ApplicationDbContext.Create())
      {
        // Create a node for each album 
        foreach (var article in db.NewsArticles)
        {
          DynamicNode dynamicNode = new DynamicNode();
          dynamicNode.Title = article.Name;
          dynamicNode.ParentKey = "News";
          dynamicNode.RouteValues.Add("urlName", article.UrlName);
          dynamicNode.LastModifiedDate = article.Modified;
          dynamicNode.ChangeFrequency = ChangeFrequency.Monthly;
          yield return dynamicNode;
        }
      }
    }
  }

  public class RentalsDynaicNodeProvider : DynamicNodeProviderBase
  {
    public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
    {
      using (var db = ApplicationDbContext.Create())
      {
        // Create a node for each album 
        foreach (var rental in db.Rentals)
        {
          DynamicNode dynamicNode = new DynamicNode();
          dynamicNode.Title = rental.Name;
          dynamicNode.ParentKey = "News";
          dynamicNode.RouteValues.Add("urlName", rental.Name);
          dynamicNode.LastModifiedDate = rental.Modified;
          dynamicNode.ChangeFrequency = ChangeFrequency.Weekly;
          yield return dynamicNode;
        }
      }
    }
  }
}