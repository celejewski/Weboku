﻿using System;
using Weboku.Application.Data;
using Weboku.Application.Exceptions;
using Weboku.Application.Interfaces;
using Weboku.Core.Data;
using Weboku.Core.Serializers;

namespace Weboku.Application.Managers
{
    internal sealed class StorageManager
    {
        private readonly IGridSerializer _gridSerializer;
        private readonly IStorageProvider _storageProvider;

        public StorageManager(IStorageProvider storageProvider)
        {
            _gridSerializer = GridSerializerFactory.Make(GridSerializerName.Base64);
            _storageProvider = storageProvider;
        }

        public void Save(StorageDto storageDto)
        {
            _storageProvider.Save(nameof(StorageDto.Grid), _gridSerializer.Serialize(storageDto.Grid));
            _storageProvider.Save(nameof(StorageDto.Difficulty), storageDto.Difficulty);
        }

        public StorageDto Load()
        {
            try
            {
                Grid grid = null;
                var difficulty = Difficulty.Unknown;
                if (_storageProvider.HasKey(nameof(StorageDto.Grid)))
                {
                    grid = _gridSerializer.Deserialize(_storageProvider.Load<string>(nameof(StorageDto.Grid)));
                }

                if (_storageProvider.HasKey(nameof(StorageDto.Difficulty)))
                {
                    difficulty = _storageProvider.Load<Difficulty>(nameof(StorageDto.Difficulty));
                }

                return new StorageDto(grid ?? new Grid(), difficulty);
            }
            catch (Exception e)
            {
                throw new LoadException("Load failed.", e);
            }
        }
    }
}