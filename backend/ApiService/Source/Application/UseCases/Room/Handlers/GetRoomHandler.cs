﻿using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Handlers
{
    public class GetRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<GetRoomQuery, Result<RoomAggregate, ValidationResult>>
    {
        public async Task<Result<RoomAggregate, ValidationResult>> Handle(GetRoomQuery request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UserCode))
            {
                return await roomRepository.GetByUserCodeAsync(request.UserCode!, cancellationToken);
            }

            return await roomRepository.GetByRoomCodeAsync(request.RoomCode!, cancellationToken);
        }
    }
}