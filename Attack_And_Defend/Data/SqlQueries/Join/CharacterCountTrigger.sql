create trigger ValidateCharacterCount on Characters
for insert, update
as
begin
	declare @CountCharactersInParty int;

	declare @IdPartyToInsertInto int;
	set @IdPartyToInsertInto = (select PartyId from inserted);

	set @CountCharactersInParty = (select COUNT(Characters.Id) from Parties inner join Characters on Characters.PartyId = Parties.Id where Parties.Id = @IdPartyToInsertInto);
	if @CountCharactersInParty > 5
	begin;
		rollback transaction;
		return;
	end;
end;