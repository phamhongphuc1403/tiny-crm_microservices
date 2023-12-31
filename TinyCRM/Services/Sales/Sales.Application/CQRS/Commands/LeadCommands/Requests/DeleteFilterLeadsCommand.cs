﻿using BuildingBlock.Application.CQRS.Command;
using Sales.Application.DTOs.LeadDTOs;

namespace Sales.Application.CQRS.Commands.LeadCommands.Requests;

public class DeleteFilterLeadsCommand : FilterLeadsDto, ICommand
{
    public DeleteFilterLeadsCommand(FilterLeadsDto dto)
    {
        Status = dto.Status;
        Keyword = dto.Keyword;
    }
}