using APIRanked.Models;
using APIRanked.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIRanked.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Lấy danh sách tất cả các Role.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Lấy thông tin Role theo ID.
        /// </summary>
        /// <param name="id">ID của Role</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var role = await _roleService.GetByIdAsync(id);
                return Ok(role);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Thêm mới một Role.
        /// </summary>
        /// <param name="role">Thông tin Role cần thêm</param>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _roleService.AddAsync(role);
            return CreatedAtAction(nameof(GetById), new { id = role.RoleId }, role);
        }

        /// <summary>
        /// Cập nhật thông tin Role.
        /// </summary>
        /// <param name="id">ID của Role cần cập nhật</param>
        /// <param name="role">Thông tin Role cập nhật</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest("ID không khớp.");
            }

            try
            {
                await _roleService.UpdateAsync(role);
                return Ok("Cập nhật thành công.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Xóa một Role theo ID.
        /// </summary>
        /// <param name="id">ID của Role cần xóa</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roleService.DeleteAsync(id);
                return Ok("Xóa thành công.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
