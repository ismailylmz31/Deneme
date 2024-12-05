using AutoMapper;
using Core.Persistence.Extensions;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.VideoEducation;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Concretes;

namespace TechCareer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoEducationController : ControllerBase
    {
        private readonly IVideoEducationService _videoEducationService;
        private readonly IMapper _mapper;

        public VideoEducationController(IVideoEducationService videoEducationService, IMapper mapper)
        {
            _videoEducationService = videoEducationService;
            _mapper = mapper;
        }

        // Tüm eğitim videoları Listeleme
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var videoeducations = await _videoEducationService.GetListAsync(cancellationToken: cancellationToken);
            var response = _mapper.Map<List<VideoEducationResponseDto>>(videoeducations);
            return Ok(response);
        }

        // id'ye göre video getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
        {
            var videoeducation = await _videoEducationService.GetByIdAsync(id, cancellationToken: cancellationToken);
            if (videoeducation == null)
                return NotFound("Video not found.");

            var response = _mapper.Map<VideoEducationResponseDto>(videoeducation);
            return Ok(response);
        }

        //Video Ekleme
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateVideoEducationRequestDto dto)
        {
            var addedVideo = await _videoEducationService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = addedVideo.Id }, addedVideo);
        }

        //Video Güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateVideoEducationRequestDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var updateVideo = await _videoEducationService.UpdateAsync(id, dto);
            return Ok(updateVideo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, bool permanent = false)
        {

            var result = await _videoEducationService.DeleteAsync(id, permanent);
            return NoContent();
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> GetPaginate(
        [FromQuery] int index = 0,
        [FromQuery] int size = 10,
        CancellationToken cancellationToken = default)
        {
            var paginatedVideo = await _videoEducationService.GetPaginateAsync(
                index: index,
                size: size,
                cancellationToken: cancellationToken
            );

            // Sonuçları DTO'ya map etmek isteyebilirsiniz
            var response = new Paginate<VideoEducationResponseDto>
            {
                Items = _mapper.Map<IList<VideoEducationResponseDto>>(paginatedVideo.Items),
                Index = paginatedVideo.Index,
                Size = paginatedVideo.Size,
                Count = paginatedVideo.Count,
                Pages = paginatedVideo.Pages
            };

            return Ok(response);
        }
    }
}