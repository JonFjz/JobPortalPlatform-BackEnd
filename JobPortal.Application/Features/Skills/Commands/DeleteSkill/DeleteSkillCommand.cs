﻿using MediatR;

namespace JobPortal.Application.Features.Skills.Commands.DeleteSkill
{
    public class DeleteSkillCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteSkillCommand(int id)
        {
            Id = id;
        }
    }
}