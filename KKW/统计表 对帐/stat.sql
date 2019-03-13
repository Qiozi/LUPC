select distinct moneystatus '类型', sum(total-returnFee) '金额',count(id) '订单数量', orderstatus from tb_order where orderstatus='交易成功' and brandno like 'T%' group by moneystatus, orderstatus
union
select distinct moneystatus, sum(total-returnFee),count(id) ,orderstatus from tb_order where orderstatus='等待对方确认收货' and brandno like 'T%' group by moneystatus, orderstatus



delete from tb_order