select ItemCode, Count from 
(select ItemCode, count(*) as Count
from dbo.Items
group by ItemCode
--having count(*)=3
) a
order by Count desc
