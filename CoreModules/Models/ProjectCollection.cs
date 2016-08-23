using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace CodeModules.Models
{
    [CollectionDataContract(Namespace = "http://www.shahedaziz.com/project/coremodules/projectcollection", IsReference = false)]
    public class ProjectCollection : ObservableCollection<ProjectModel>
    {
    }
}