using System.Collections.Generic;

namespace ImgProcessing
{
    public interface IProcessingOperation
    {
        void Execute(IEnumerable<ProcessingPart> parts, IEnumerable<IOperation> operations);
    }
}
