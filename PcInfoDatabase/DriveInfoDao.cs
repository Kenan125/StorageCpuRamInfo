using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcInfoDatabase
{
    public class DriveInfoDao
    {
        private readonly PcInfoRepository _pcInfoRepository;

        public DriveInfoDao(PcInfoRepository pcInfoRepository)
        {
            _pcInfoRepository = pcInfoRepository;
        }

        public void SaveDriveInfo(int pcId, string driveInfo)
        {
            _pcInfoRepository.InsertDriveInfo(pcId, driveInfo, DateTime.Now);
        }
    }
}
