using AutoMapper;
using BuildingBlock.Application.CQRS.Command;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Repositories;
using People.Application.CQRS.Commands.ContactCommands.Requests;
using People.Application.DTOs.ContactDTOs;
using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;
using People.Domain.ContactAggregate.Services;

namespace People.Application.CQRS.Commands.ContactCommands.Handlers;

public class CreateContactCommandHandler : ICommandHandler<CreateContactCommand, ContactDetailDto>
{
    private readonly IReadOnlyRepository<Account> _accountReadOnlyRepository;
    private readonly IOperationRepository<Contact> _contactOperationRepository;
    private readonly IContactService _contactService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateContactCommandHandler(IContactService contactService, IMapper mapper,
        IOperationRepository<Contact> contactOperationRepository, IUnitOfWork unitOfWork,
        IReadOnlyRepository<Account> accountReadOnlyRepository)
    {
        _contactService = contactService;
        _mapper = mapper;
        _contactOperationRepository = contactOperationRepository;
        _unitOfWork = unitOfWork;
        _accountReadOnlyRepository = accountReadOnlyRepository;
    }

    public async Task<ContactDetailDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactService.CreateAsync(request.Name, request.Email, request.Phone, request.AccountId);

        await _contactOperationRepository.AddAsync(contact);

        await _unitOfWork.SaveChangesAsync();

        await _contactService.AddAccount(contact, request.AccountId);

        return _mapper.Map<ContactDetailDto>(contact);
    }
}