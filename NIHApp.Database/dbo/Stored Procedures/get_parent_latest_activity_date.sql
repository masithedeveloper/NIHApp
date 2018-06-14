CREATE procedure [dbo].[get_parent_latest_activity_date]
(
	@parentId int
) as
	 begin
		select Top(1) a.* 
		from [dbo].[ApiSession] as a with (nolock)	
		join [dbo].[Parent] as b on b.ParentId= a.[subscriber_id]
		where b.ParentId = @parentId 
		order by a.[generated_on] Desc
	end