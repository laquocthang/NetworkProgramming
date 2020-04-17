using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WideWorldImporters.API.UnitTests
{
	public class Others
	{
		/// <summary>
		/// Tìm kiếm Stock Items theo các tham số như lastEditedBy, colorID, outerPackageID, supplierID, unitPackageID.
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItems()
		{

		}

		/// <summary>
		/// Lấy Stock Item với đầu vào là ID không tồn tại, kiểm tra Web API trả về trạng thái NotFound(404)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetNonExistingStockItem()
		{

		}

		/// <summary>
		/// Thêm một Stock Item mà mẫu tin đó đã tồn tại trong hệ thống, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestAddStockItemWithExistingName()
		{

		}

		/// <summary>
		/// Thêm vào một Stock Item mà bỏ trống các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestAddStockItemWithoutRequiredFields()
		{

		}

		/// <summary>
		/// Cập nhật một Stock Item sử dụng một ID không tồn tại, kiểm tra Web API returns NotFound(404) status.
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestUpdateNonExistingStockItem()
		{

		}

		/// <summary>
		/// Cập nhật một Stock Item đã tồn tại mà bỏ trồng các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400).
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestUpdateExistingStockItemWithoutRequiredFields()
		{

		}

		/// <summary>
		/// Xóa một Stock Item sử dụng một ID không tồn tại và kiểm tra Web API trả về trạng thái NotFound(404).
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestDeleteNonExistingStockItem()
		{

		}
	}
}
