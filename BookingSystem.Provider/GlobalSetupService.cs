using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.GlobalSetupDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class GlobalSetupService : BaseService
    {
        private BookingSystemContext _context;

        public GlobalSetupService(BookingSystemContext _context)
        {
            this._context = _context;
        }

        public GSIndexDTO GetAll(string parameterCode, string parameterName, int page)
        {
            var dto = new GSIndexDTO()
            {
                Grid = new List<GSRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstGlobalSetups in dbContext.MstGlobalSetups
                            where 
                            (
                                mstGlobalSetups.ParameterCode.Trim().ToLower().Contains(parameterCode.Trim().ToLower())
                                && mstGlobalSetups.ParameterName.Trim().ToLower().Contains(parameterName.Trim().ToLower())
                            )
                            && mstGlobalSetups.DeletedDate == null
                            select new GSRowDTO()
                            {
                                GSID = mstGlobalSetups.Id,
                                ParameterCode = mstGlobalSetups.ParameterCode,
                                ParameterName = mstGlobalSetups.ParameterName,
                                ParameterDesc = mstGlobalSetups.ParameterDesc,
                                ParameterValue = mstGlobalSetups.ParameterValue
                            };

                int totalRecords = query.Count();
                if (page > 0)
                {
                    dto.TotalPages = GetTotalPages(totalRecords);
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                }

                dto.Grid = query.ToList();
            }

            return dto;
        }

        public MstGlobalSetup GetOne(int gsId)
        {
            return _context.MstGlobalSetups.SingleOrDefault(a => a.Id == gsId);
        }

        public void InsertGS(InsertGSDTO model)
        {
            var newGS = new MstGlobalSetup();
            //newGS.Id = model.GSID.Value;
            newGS.ParameterCode = model.ParameterCode;
            newGS.ParameterName = model.ParameterName;
            newGS.ParameterDesc = model.ParameterDesc;
            newGS.ParameterValue = model.ParameterValue;
            newGS.CreatedBy = 1;
            newGS.CreatedDate = DateTime.UtcNow;

            _context.MstGlobalSetups.Add(newGS);
            _context.SaveChanges();
        }

        public void UpdateGS(UpdateGSDTO model)
        {
            var existedGS = GetOne(model.GSID);
            existedGS.ParameterCode = model.ParameterCode;
            existedGS.ParameterName = model.ParameterName;
            existedGS.ParameterDesc = model.ParameterDesc;
            existedGS.ParameterValue = model.ParameterValue;
            existedGS.UpdatedBy = 2;
            existedGS.UpdatedDate = DateTime.UtcNow;

            _context.SaveChanges();
        }

        public void DeleteGS(int gsId)
        {
            var newGS = GetOne(gsId);
            newGS.DeletedBy = 3;
            newGS.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
