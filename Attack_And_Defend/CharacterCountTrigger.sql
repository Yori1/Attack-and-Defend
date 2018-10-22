create trigger ValidateCharacterCount on Characters
for insert
as
declare @CountCharactersInParty int;

declare @IdPartyToInsertInto int;
set @IdPartyToInsertInto = (select Parties.Id from inserted inner join Parties on inserted.PartyId = Parties.Id);

set @CountCharactersInParty = (select COUNT(Characters.Id) from Parties inner join Characters on Characters.PartyId = Parties.Id where Parties.Id = @IdPartyToInsertInto);
if @CountCharactersInParty > 5
begin;
rollback transaction;
return;
end;