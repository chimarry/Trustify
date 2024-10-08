﻿using AutoMapper;
using Trustify.Backend.FeaturesCore.AutoMapper;

namespace Trustify.Backend.AdminService.AutoMapper
{
    /// <summary>
    /// Encapsulates mapper logic related to objects of a type ResultMessage <see cref="ResultMessage{T}"/>
    /// </summary>
    public static class ResultMessageExtensionMethods
    {
        /// <summary>
        /// Because ResultMessage is a generic class, this method allows user to convert from ResultMessage<typeparamref name="OriginalType"/>
        /// to ResultMessage<typeparamref name="WrapperType"/>, thus mapping operation result from OriginalType to Wrapper. It can be used for 
        /// wrapping primitive operation results in class objects.
        /// </summary>
        /// <typeparam name="WrapperType">Type to convert to</typeparam>
        /// <typeparam name="OriginalType">Original type</typeparam>
        /// <param name="resultMessage">Object that is mapped</param>
        /// <param name="_mapper">Injected AutoMapper mapper</param>
        /// <returns>New ResultMessage object</returns>
        public static ResultMessage<WrapperType> Map<WrapperType, OriginalType>(this ResultMessage<OriginalType> resultMessage, IMapper _mapper)
             => _mapper.Map<ResultMessage<OriginalType>, ResultMessage<WrapperType>>(resultMessage);
    }
}
