SELECT 
	vn.Name + '|' + CAST(vn.Price AS VARCHAR(MAX)) + '|' + vn.ProductCodes,
	vn.Qty
FROM 
(
	SELECT TOP 1000
		d.Name,
		COUNT(d.Name) AS Qty,
		(
			SELECT LEFT(pc.ProductCodes, LEN(pc.ProductCodes) - 1)
			FROM 
			(
				SELECT 
				(
					SELECT DefaultProductCode + ',' AS [text()]
					FROM voucher.DealItem
					WHERE DealId = MIN(di.DealId)
					FOR XML PATH ('')
				) AS ProductCodes
			) pc
		) AS ProductCodes,
		MIN(d.Value) AS Price
	FROM voucher.VoucherInstance vi
	JOIN voucher.Voucher v 
	ON v.VoucherId = vi.VoucherId
	JOIN voucher.Deal d
	ON d.DealId = v.DealId
	AND d.Value IS NOT NULL
	AND d.Value > 0
	JOIN voucher.DealItem di
	ON di.DealId = d.DealId
	AND di.DefaultProductCode IS NOT NULL
	GROUP BY d.Name
	HAVING COUNT(di.DefaultProductCode) > 0
	ORDER BY COUNT(d.Name) DESC
) vn