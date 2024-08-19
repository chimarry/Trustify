using AutoMapper;
using Trustify.Backend.FeaturesCore.AutoMapper;
using Trustify.Backend.FeaturesCore.DTO;

namespace Trustify.Backend.FeaturesService.Mapper
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

        /// <summary>
        /// Get file information from result message, such as file name and file data, or return empty values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        public static (string name, byte[] data) GetFileInfo<T>(this ResultMessage<T> resultMessage)
        {
            BasicFileInfo? fileInfo = null;
            if (resultMessage.Result != null)
                fileInfo = resultMessage.Result as BasicFileInfo;

            if (fileInfo != null)
                return (fileInfo.FileName, fileInfo.FileData);

            return (string.Empty, Array.Empty<byte>());
        }
    }
}
