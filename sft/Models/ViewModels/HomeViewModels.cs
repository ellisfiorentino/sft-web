using System.Collections.Generic;

namespace sft.Models.ViewModels
{
  public class HomeViewModel
  {
    public List<FeatureViewModel> Featured { get; set; }
    public List<FeatureRentalViewModel> FeatureRentals { get; set; } 



    public HomeViewModel()
    {
      Featured = new List<FeatureViewModel>();
      FeatureRentals = new List<FeatureRentalViewModel>();
    }

  }

  public class FeatureViewModel
  {
    public string LinkLocation { get; set; }
    public string ImgLocation { get; set; }
    public string Header { get; set; }
    public string SubHeader { get; set; }
    public string SmallText { get; set; }
    public int Comments { get; set; }
  }

  public class FeatureRentalViewModel
  {
    public string LinkLocation { get; set; }
    public string ImageLocation { get; set; }
  }

}