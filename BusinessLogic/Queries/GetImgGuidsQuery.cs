using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Queries;

public record GetImgGuidsQuery() : IRequest<ImgGuidsResponse>; 