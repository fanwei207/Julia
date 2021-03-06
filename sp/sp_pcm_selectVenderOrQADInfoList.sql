USE [tcpc0]
GO
/****** Object:  StoredProcedure [dbo].[sp_pcm_selectVenderOrQADInfoList]    Script Date: 2015/6/24 9:49:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<fanwei>
-- Create date: <20150402>
-- Description:	<检测和查找有关上传数据的全部信息>
-- =============================================
ALTER PROCEDURE [dbo].[sp_pcm_selectVenderOrQADInfoList]
	@gvTable XML
	,@uID int
AS
BEGIN
	
	DECLARE @err INT 
			,@flag INT
			SET @err=0
	DECLARE @tableTemp TABLE (
						[PQID] [char](9) NULL,
						[QAD] [varchar](20) NULL,
						[vender] [varchar](20) NULL,
						[formate] [nvarchar](400) NULL,
						[um] [varchar](2) NULL,
						[curr] [varchar](5) NULL,
						[applyPrice] [varchar](20) NULL,
						[applyPriceStarDate] [nvarchar](50) NULL,
						[applyPriceEndDate] [nvarchar](50) NULL,
						[oldPrice] [varchar](20) NULL,
						[oldPriceStarDate] [nvarchar](50) NULL,
						[oldPriceEndDate] [nvarchar](50) NULL,
						[venderName] [nvarchar](100) NULL,
						ERROR NVARCHAR(500),
						GUID VARCHAR(50) ,
						createdBy INT,
						createdDate datetime
						)

	
		INSERT INTO @tableTemp
		        ( PQID ,
		          QAD ,
		          vender ,
		          formate ,
		          um ,
		          curr ,
		          applyPrice ,
		          applyPriceStarDate ,
		          applyPriceEndDate ,
		          oldPrice ,
		          oldPriceStarDate ,
		          oldPriceEndDate ,
		          venderName ,
		          GUID  ,
		          createdBy ,
		          createdDate
		        )
		SELECT Tab.Col.value('PQID[1]','char(9)')
			   ,Tab.Col.value('QAD[1]','varchar(20)')
			   ,Tab.Col.value('vender[1]','varchar(20)')
			   ,Tab.Col.value('formate[1]','nvarchar(400)')
			   ,Tab.Col.value('um[1]','varchar(2)')
			   ,Tab.Col.value('curr[1]','varchar(5)')
			   ,Tab.Col.value('applyPrice[1]','varchar(20)')
			   ,Tab.Col.value('applyPriceStarDate[1]','nvarchar(50)')
			   ,Tab.Col.value('applyPriceEndDate[1]','nvarchar(50)')
			   ,Tab.Col.value('oldPrice[1]','varchar(20)')
			   ,Tab.Col.value('oldPriceStarDate[1]','nvarchar(50)')
			   ,Tab.Col.value('oldPriceEndDate[1]','nvarchar(50)')
			   ,Tab.Col.value('venderName[1]','nvarchar(100)')
			   ,Tab.Col.value('GUID[1]','varchar(50)')
			   ,@uID
			   ,GETDATE()
			   from  @gvTable.nodes('//TempTable')as Tab(Col)
			   
			--UPDATE @tableTemp
			--SET QAD =REPLACE(QAD,'*',' % ')
			--IF(@@ROWCOUNT=0)
			--BEGIN
			--END
			
			update @tableTemp
			set error =isnull(error,N'') +N'QAD号不存在;'
			where  (NOT EXISTS  (select item_qad  from tcpc0.dbo.Items WHERE QAD=item_qad	) 
				OR NOT EXISTS (SELECT pt_part FROM QAD_data.dbo.pt_mstr WHERE QAD = pt_part))
			and  createdBy =@uID AND ISNULL(QAD,'')<>''
			IF(@@ROWCOUNT=0)
			BEGIN
			update @tableTemp
			set error =isnull(error,N'') +N'供应商号不存在;'
			where  not  EXISTS(
									select ad_addr   
									from QAD_Data..ad_mstr 
									where ad_type = 'supplier' AND ad_addr=vender
									
									)
				and createdBy =@uID
				AND ISNULL(vender,'')<>''
			IF(@@ROWCOUNT=0)
			BEGIN
			
			--供应商无法找到历史数据
			UPDATE ait
			SET error =isnull(error,N'') +N'供应商无法找到历史数据;'
			FROM @tableTemp ait
			LEFT JOIN tcpc0.dbo.PC_mstr pc ON ait.vender=pc.pc_list
			WHERE ISNULL(pc.pc_part,'')=''
			and isnull(ait.QAD,'')=''
			and isnull(ait.vender,'')<>''
			IF(@@ROWCOUNT=0)
			BEGIN
			
			--QAD号无历史数据
			UPDATE ait
			SET error =isnull(error,N'') +N'QAD号无历史数据;'
			FROM @tableTemp ait  LEFT JOIN tcpc0.dbo.PC_mstr pc ON ait.QAD=pc.pc_part 
			WHERE ISNULL(pc.pc_list,'')=''
			and isnull(ait.QAD,'')<>'' 
			and isnull(ait.vender,'')=''
			IF(@@ROWCOUNT=0)
			BEGIN
			
				--QAD号与供应商组合无法找到历史数据
			UPDATE ait
			SET error =ISNULL(error,N'') +N'QAD号与供应商组合无法找到历史数据;'
			from @tableTemp ait LEFT JOIN tcpc0.dbo.PC_mstr pc ON ait.QAD=pc.pc_part 
			AND ait.vender =pc.pc_list
			WHERE ISNULL(ait.QAD,'') <> '' 
			and ISNULL(ait.vender,'') <> ''
			and (ISNULL(pc.pc_part,'') = '' 
			or ISNULL(pc.pc_list,'') = '' )
			IF(@@ROWCOUNT=0)
			BEGIN
				SET @err=1
				BEGIN TRAN importApply
				INSERT INTO dbo.PCM_applyImportTemp
					        ( PQID ,
					          QAD ,
					          vender ,
					          um ,
					          curr ,
					          oldPrice ,
					          oldPriceStarDate ,
					          oldPriceEndDate ,
					          venderName ,
					          createdBy ,
					          createdDate 
					        )
						SELECT son.PQID,son.QAD,son.vender,son.pc_um,son.pc_curr,son.pc_price,CONVERT(VARCHAR(10),son.pc_start,120),CONVERT(VARCHAR(10),son.pc_expire,120),son.ad_name,@uID,GETDATE()
						FROM (	
								SELECT ait.PQID,ait.QAD,ait.vender,pc.pc_um,pc.pc_curr,ad.ad_name,pc_start
								,pc_expire,Row_Number() OVER (partition by pc_part,pc_list ORDER BY  ISNULL(pc.pc_expire,'3000-1-1' ) desc ,pc.pc_start desc) AS number
								,pc.pc_price AS pc_price
								FROM @tableTemp ait 
								LEFT JOIN tcpc0.dbo.PC_mstr pc 
								ON ait.QAD=pc.pc_part AND ait.vender = pc.pc_list
								LEFT JOIN (
									SELECT ad_domain,ad_addr,MAX(ad_name) ad_name,ad_type
									from QAD_Data.dbo.ad_mstr
									GROUP BY ad_domain,ad_addr,ad_type
								) ad ON pc.pc_list=ad.ad_addr  AND pc.pc_domain=ad.ad_domain
								LEFT JOIN dbo.PCM_ApplyDet pad 
								ON pad.Part=pc.pc_part AND pad.Vender=pc.pc_list 
								LEFT JOIN tcpc0.dbo.PC_ApplyDet tad
								ON tad.Part=ait.QAD AND tad.Vender=ait.vender
								WHERE ISNULL(ait.QAD,'')<>'' 
									AND ISNULL(ait.vender,'')<>''
									AND ad.ad_type = 'supplier'
									AND (ISNULL(pad.Part,'')='' OR( ISNULL(pad.Part,'')<>'' AND (pad.status<=0 OR pad.status=6 OR pad.isCancel=1))
									AND NOT  EXISTS	(SELECT *
													FROM dbo.pcu_part_vend_type
													WHERE pcutv_part = ait.QAD
													AND pcutv_vend = ait.vender
													)
								))AS son
							WHERE son.number=1 
							
							
				INSERT INTO PCM_applyImportTemp--插入只有供应商的数据
					        ( PQID ,
					          QAD ,
					          vender ,
					          um ,
					          curr ,
					          oldPrice ,
					          oldPriceStarDate ,
					          oldPriceEndDate ,
					          venderName ,
					          createdBy ,
					          createdDate 
					        )
						SELECT son.PQID,son.QAD,son.vender,son.pc_um,son.pc_curr,son.pc_price,CONVERT(VARCHAR(10),son.pc_start,120),CONVERT(VARCHAR(10),son.pc_expire,120),son.ad_name,@uID,GETDATE()
						FROM (	
								SELECT ait.PQID,pc.pc_part AS QAD,ait.vender,pc.pc_um,pc.pc_curr,ad.ad_name ,pc_start
								,pc_expire,Row_Number() OVER (partition by pc_part,pc_list ORDER BY  ISNULL(pc.pc_expire,'3000-1-1' ) desc ,pc.pc_start desc) AS number
								,pc.pc_price AS pc_price
								FROM @tableTemp ait 
								LEFT JOIN tcpc0.dbo.PC_mstr pc 
								ON  ait.vender = pc.pc_list
								LEFT JOIN (
									SELECT ad_domain,ad_addr,MAX(ad_name) ad_name,ad_type
									from QAD_Data.dbo.ad_mstr
									GROUP BY ad_addr,ad_type,ad_domain
								) ad ON pc.pc_list=ad.ad_addr  AND pc.pc_domain=ad.ad_domain
								LEFT JOIN dbo.PCM_ApplyDet pad 
								ON pad.Part=pc.pc_part AND pad.Vender=pc.pc_list 
								LEFT JOIN tcpc0.dbo.PC_ApplyDet tad
								ON tad.Part=ait.QAD AND tad.Vender=ait.vender
								WHERE ISNULL(ait.QAD,'')='' 
									AND ISNULL(ait.vender,'')<>''
									AND ad.ad_type = 'supplier'
									AND (ISNULL(pad.Part,'')='' OR( ISNULL(pad.Part,'')<>'' AND (pad.status<=0 OR pad.status=6 OR pad.isCancel=1))
									AND NOT  EXISTS	(SELECT *
													FROM dbo.pcu_part_vend_type
													WHERE pcutv_part = ait.QAD
													AND pcutv_vend = ait.vender
													)
								))AS son
							WHERE son.number=1 
							
							
								
				INSERT INTO PCM_applyImportTemp--插入只有QAD的数据
								 ( PQID ,
								  QAD ,
								  vender ,
								  um ,
								  curr ,
								  oldPrice ,
								  oldPriceStarDate ,
								  oldPriceEndDate ,
								  venderName ,
								  createdBy ,
								  createdDate 
								)
						SELECT son.PQID,son.QAD,son.vender,son.pc_um,son.pc_curr,son.pc_price,CONVERT(VARCHAR(10),son.pc_start,120),CONVERT(VARCHAR(10),son.pc_expire,120),son.ad_name,@uID,GETDATE()
						FROM (	
								SELECT ait.PQID,ait.QAD,ad.ad_addr AS vender,pc.pc_um,pc.pc_curr,ad.ad_name,pc_start
								,pc_expire,Row_Number() OVER (partition by pc_part,pc_list ORDER BY ISNULL(pc.pc_expire,'3000-1-1' ) desc ,pc.pc_start desc) AS number
								,pc.pc_price AS pc_price
								FROM @tableTemp ait 
								LEFT JOIN tcpc0.dbo.PC_mstr pc 
								ON ait.QAD=pc.pc_part 
								LEFT JOIN (
									SELECT ad_domain,ad_addr,MAX(ad_name) ad_name,ad_type
									from QAD_Data.dbo.ad_mstr
									GROUP BY ad_domain,ad_addr,ad_type
								) ad ON pc.pc_list=ad.ad_addr  AND pc.pc_domain=ad.ad_domain
								LEFT JOIN dbo.PCM_ApplyDet pad 
								ON pad.Part=pc.pc_part AND pad.Vender=pc.pc_list 
								LEFT JOIN tcpc0.dbo.PC_ApplyDet tad
								ON tad.Part=ait.QAD AND tad.Vender=ait.vender
								WHERE ISNULL(ait.QAD,'')<>'' 
									AND ISNULL(ait.vender,'')=''
									AND ad.ad_type = 'supplier'
									AND (ISNULL(pad.Part,'')='' OR( ISNULL(pad.Part,'')<>'' AND (pad.status<=0 OR pad.status=6 OR pad.isCancel=1))
									AND NOT  EXISTS	(SELECT *
													FROM dbo.pcu_part_vend_type
													WHERE pcutv_part = ait.QAD
													AND pcutv_vend = ait.vender
													)
								))AS son
							WHERE son.number=1 

							
							
					UPDATE ait
					SET ait.formate= (CASE WHEN ISNULL(mad.Formate,N'')=N'' AND ISNULL(ad.Formate,N'')=N'' THEN N''
										WHEN ISNULL(mad.Formate,N'')=N'' AND ISNULL(ad.Formate,N'')<>N'' THEN ad.Formate
										WHEN ISNULL(mad.Formate,N'')<>N'' AND ISNULL(ad.Formate,N'')<>N'' THEN mad.Formate
										WHEN ISNULL(mad.Formate,N'')<>N'' AND ISNULL(ad.Formate,N'')=N'' THEN mad.Formate
										END
										 )
					FROM PCM_applyImportTemp ait
					LEFT JOIN tcpc0.dbo.PC_ApplyDet ad
					ON ait.QAD=ad.Part AND ait.vender=ad.Vender 
					LEFT JOIN dbo.PCM_ApplyDet mad 
					ON ait.QAD=mad.Part AND ait.vender=mad.Vender 
					
					
					IF(@@error <>0 )
					BEGIN
						ROLLBACK TRAN importApply
						SELECT 0	
					END
					else
					begin
						SELECT  PQID,QAD,vender,formate,um,curr,oldPrice
						,CONVERT(VARCHAR(10),oldPriceStarDate,120) as  oldPriceStarDate
						,CONVERT(VARCHAR(10),oldPriceEndDate,120) as oldPriceEndDate
						,applyPrice
						,CONVERT(VARCHAR(10),applyPriceStarDate,120) as applyPriceStarDate
						,CONVERT(VARCHAR(10),applyPriceEndDate,120) as applyPriceEndDate
						,venderName,GUID
						FROM dbo.PCM_applyImportTemp
						WHERE createdBy = @uID
						COMMIT TRAN importApply
						
					END		
			END
			END
			END
			END
			END
		
			IF(@err<>1)
			BEGIN
				SELECT  PQID,QAD,vender,formate,um,curr,oldPrice
					,CONVERT(VARCHAR(10),oldPriceStarDate,120) as  oldPriceStarDate
					,CONVERT(VARCHAR(10),oldPriceEndDate,120) as oldPriceEndDate
					,applyPrice
					,CONVERT(VARCHAR(10),applyPriceStarDate,120) as applyPriceStarDate
					,CONVERT(VARCHAR(10),applyPriceEndDate,120) as applyPriceEndDate,venderName
					,GUID,error
					FROM @tableTemp
					WHERE createdBy = @uID
			END
END

