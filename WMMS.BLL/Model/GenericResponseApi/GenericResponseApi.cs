using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMMS.BLL.Model.DTO_s.SaleDTO_s;

namespace WMMS.BLL.Model.GenericResponseApi
{
	public class GenericResponseApi<T>
	{
		public T Data { get; set; }
		public bool IsSuccess {  get; set; }
		public string ErrorMessage {  get; set; }
		public int StatusCode {  get; set; }
		public List<string> Errors { get; set; }


        public GenericResponseApi()
        {
            Errors = new List<string>();
        }

		public GenericResponseApi(T data)
		{
			Data = data;
			IsSuccess = true;
			StatusCode = 200;
		}

		public GenericResponseApi(List<string> errors) : this()
		{
			Errors = errors;
			IsSuccess = false;
			StatusCode = 400;
		}

		public void Success(T data, int StatusCode = 200)
		{
			IsSuccess = true;
			Errors = null;
			Data = data;
			this.StatusCode = StatusCode;
		}

		public void Failure(List<string> errors, int StatusCode = 500)
		{
			IsSuccess = false;
			Data = default;
			Errors = errors;
			this.StatusCode = StatusCode;
		}


		public void Failure(string errors, int StatusCode = 500)
		{
			IsSuccess = false;
			Data = default;
			Errors = new List<string> { errors };
			this.StatusCode = StatusCode;
		}

	}  
}
