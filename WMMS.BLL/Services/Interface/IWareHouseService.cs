using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.WareHouseDTO_s;
using WMMS.BLL.Model.GenericResponseApi;

namespace WMMS.BLL.Services.Interface
{
	public interface IWareHouseService
	{
		Task<GenericResponseApi<List<GetWareHouseDTO>>> GetWareHouse();
		Task<GenericResponseApi<bool>> CreateWareHouse(CreateWareHouseDTO createWareHouse);
		Task<GenericResponseApi<bool>> UpdateWareHouse(UpdateWareHouseDTO updateWareHouse);
		Task<GenericResponseApi<bool>> DeleteWareHouse(int id);
	}
}
