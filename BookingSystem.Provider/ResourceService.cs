using BookingSystem.DataAccess.Models;
using BookingSystem.DataModel.Master.BookingCodeVM;
using BookingSystem.DTO.Child.ChildResource;
using BookingSystem.DTO.Master.BookingCodeDTO;
using BookingSystem.DTO.Master.LocationDTO;
using BookingSystem.DTO.Master.ResourceDTO;
using BookingSystem.DTO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public class ResourceService : BaseService
    {
        private BookingSystemContext _context;

        public ResourceService(BookingSystemContext _context)
        {
            this._context = _context;
        }

        public ResourceIndexDTO GetAll(string resourceName, bool? status, int page)
        {
            var dto = new ResourceIndexDTO()
            {
                Grid = new List<ResourceRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                // Prepare the base query
                var query = from mstResources in dbContext.MstResources
                            where mstResources.ResourceName.Trim().ToLower().Contains(resourceName.Trim().ToLower())
                            && mstResources.DeletedDate == null
                            select new ResourceRowDTO()
                            {
                                Id = mstResources.Id,
                                ResourceName = mstResources.ResourceName,
                                Status = (bool)mstResources.Status,
                                StringStatus = ((bool)mstResources.Status == true ? "Available" : "Not Available"),
                                IconPath = mstResources.IconPath,
                                TotalResourceChildren = mstResources.ChlResources.Count
                            };

                // Apply status filter if provided
                if (status.HasValue)
                {
                    query = query.Where(x => x.Status == status.Value);
                }

                // Count the total number of records
                int totalRecords = query.Count();

                // Apply pagination
                if (page > 0)
                {
                    dto.TotalPages = GetTotalPages(totalRecords);
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                }

                // Execute the query and populate the DTO
                dto.Grid = query.ToList();
            }

            return dto;
        }

        public MstResource GetOne(int resourceId)
        {
            return _context.MstResources.SingleOrDefault(a => a.Id == resourceId);
        }

        public void InsertResource(InsertResourceDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var newRes = new MstResource
            {
                ResourceName = dto.ResourceName,
                Status = dto.Status,
                IconPath = dto.IconPath,
                CreatedBy = 1, // Replace with dynamic value if available
                CreatedDate = DateTime.UtcNow
            };

            _context.MstResources.Add(newRes);
            _context.SaveChanges();
        }

        public void UpdateResource(UpdateResourceDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (!dto.Id.HasValue)
            {
                throw new ArgumentException("Resource ID is required for update.");
            }

            var existingRes = GetOne(dto.Id.Value);
            if (existingRes == null)
            {
                throw new InvalidOperationException($"Resource with ID {dto.Id.Value} not found.");
            }

            existingRes.ResourceName = dto.ResourceName;
            existingRes.Status = dto.Status;
            existingRes.IconPath = dto.IconPath;
            existingRes.UpdatedBy = 2; // Replace with dynamic value if available
            existingRes.UpdatedDate = DateTime.UtcNow;

            _context.SaveChanges();
        }


        public void DeleteResource(int resourceId)
        {
            var newRes = GetOne(resourceId);
            newRes.DeletedBy = 3;
            newRes.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public ChildResourceIndexDTO GetAllChildResource(int resourceId, string resourceCode, bool? status, int page)
        {
            var dto = new ChildResourceIndexDTO()
            {
                Grid = new List<ChildResourceRowDTO>()
            };

            using (var dbContext = new BookingSystemContext())
            {
                // Prepare the base query
                var query = from chlResources in dbContext.ChlResources
                            where chlResources.ResourceId == resourceId
                            && chlResources.ResourceCode.Trim().ToLower().Contains(resourceCode.Trim().ToLower())
                            && chlResources.DeletedDate == null
                            select new ChildResourceRowDTO()
                            {
                                ResourceId = chlResources.ResourceId,
                                ResourceCode = chlResources.ResourceCode,
                                Status = (chlResources.Status.Value == true ? "Active" : "Deactive"),
                                ResourceName = chlResources.Resource.ResourceName
                            };

                // Apply status filter if provided
                if (status.HasValue)
                {
                    string statusString = (status.Value == true ? "Active" : "Deactive");
                    query = query.Where(x => x.Status.Trim().ToLower() == statusString.Trim().ToLower());
                }

                // Count the total number of records
                int totalRecords = query.Count();

                // Apply pagination
                if (page > 0)
                {
                    dto.TotalPages = GetTotalPages(totalRecords);
                    query = query.Skip(GetSkip(page)).Take(_totalPage);
                }

                // Execute the query and populate the DTO
                dto.Grid = query.ToList();
            }

            return dto;
        }

        public ChlResource GetOneChildResource(int resourceId, string chlResourceCode)
        {
            return _context.ChlResources.SingleOrDefault(a => a.ResourceId == resourceId && a.ResourceCode.Trim().ToLower() == chlResourceCode.Trim().ToLower());
        }

        public List<InsertChildResourceDTO> GetChildrenResource(int resourceId)
        {
            using (var dbContext = new BookingSystemContext())
            {
                // Prepare the base query
                var query = from chlResources in dbContext.ChlResources
                            where chlResources.ResourceId == resourceId
                                  && chlResources.DeletedDate == null
                            select new InsertChildResourceDTO()
                            {
                                ResourceId = chlResources.ResourceId,
                                ResourceCode = chlResources.ResourceCode,
                                Status = chlResources.Status.Value
                            };

                // Execute the query and return the result
                return query.ToList();
            }
        }

        public void InsertChildResource(InsertChildResourceDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var newChlRes = new ChlResource
            {
                ResourceCode = dto.ResourceCode,
                Status = dto.Status,
                ResourceId = dto.ResourceId,
                CreatedBy = 1, // Replace with dynamic value if available
                CreatedDate = DateTime.UtcNow
            };

            _context.ChlResources.Add(newChlRes);
            _context.SaveChanges();
        }

        public void UpdateChildResource(UpdateChildResourceDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (string.IsNullOrEmpty(dto.ResourceCode))
            {
                throw new ArgumentException("Resource Code is required for update.");
            }

            var existingChlRes = GetOneChildResource(dto.ResourceId, dto.ResourceCode);
            if (existingChlRes == null)
            {
                throw new InvalidOperationException($"Child resource with code {dto.ResourceCode} and master resource id {dto.ResourceId} not found.");
            }

            existingChlRes.ResourceCode = dto.ResourceCode;
            existingChlRes.Status = dto.Status;
            existingChlRes.ResourceId = dto.ResourceId;
            existingChlRes.UpdatedBy = 2; // Replace with dynamic value if available
            existingChlRes.UpdatedDate = DateTime.UtcNow;

            _context.SaveChanges();
        }


        public void DeleteChildResource(int resourceId, string chlResourceId)
        {
            var newChlRes = GetOneChildResource(resourceId, chlResourceId);
            newChlRes.DeletedBy = 3;
            newChlRes.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();
        }

        public List<MstResDropDownDTO> MstResourceList()
        {
            List<MstResDropDownDTO> dtoList;
            using (var dbContext = new BookingSystemContext())
            {
                var query = from mstRes in dbContext.MstResources
                            where mstRes.DeletedDate == null
                            select new MstResDropDownDTO()
                            {
                                chlResList = mstRes.ChlResources
                                    .Select(chlRes => new ChlResDropDownDTO
                                    {
                                        ResCode = chlRes.ResourceCode,
                                        Name = chlRes.ResourceCode
                                    }).ToList()
                            };
                dtoList = query.ToList();
            }
            return dtoList;
        }

        public List<ReturnChlResCheckBoxDTO> ReturnChlResourceList()
        {
            List<ReturnChlResCheckBoxDTO> dtoList;
            using (var dbContext = new BookingSystemContext())
            {
                var query = from chlRes in dbContext.ChlResources
                            where chlRes.DeletedDate == null && chlRes.Status == true
                            select new ReturnChlResCheckBoxDTO()
                            {
                                Value = chlRes.ResourceCode,
                                Text = chlRes.Resource.ResourceName,
                                IsChecked = false
                            };
                dtoList = query.ToList();
            }
            return dtoList;
        }
    }
}
