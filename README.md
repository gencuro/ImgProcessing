# ImgProcessing
Project represents API to load and process image by processing parts. Allowes to call processing operations and also create ones using API mechanism. By default contains two operations: convert to grayscale and split image to files.
Operation could be proccesed "by parts" and "by operations". In case of processing "by parts" all operations executes for each part, in case of processing "by operations" - all parts for each operation.

# Task
* Write algorithm NET/C# to support partial processing of the images.
* Input is quite big colorful image.
* Develop examples of partial processing callbacks.
* API should allow define input image, partial processing callback(s), type of processing "by operations" or "by parts" and processing options aka allowed processing size in pixels, part size in pixels and any other callback specific parameters. In case of processing by parts you should process all operations for each part, in case of processing by operations you should process all parts for each operation.
* Partial processing callbacks should be integral class hierarchy, which implements specifc partial processing routines. At minimum 2 specifc partial processing callbacks should be implemented : conversion of image to grayscale and splitting of image to multiple files. System design should be extendable to add additional partial processing callbacks without modification of core logic and API.
* API thould be thread safe.
* Solution should be optimized by speed and memory usage.
* Aware maximal processing size constraint, allowing part size bigger than maximal processing size. For example : you should split input image on 1000x500 parts but have only 300x300 buffer for processing.
* Unit tests should be developed, using nunit with good coverage.
* Avoid usage of third party libraries or code.

# Description
Firstly System.Drawing.Bitmap was used as a first step. But this solution very time and memory consuming. Tried to use different BitmapDecoder/BitmapEncoder but without any success. For now this approach is kept.

To splitting image on parts was decided to not split image itself but create System.Drawing.Rectange on each part. Using this rectangle possible to navigate to needed part and perform any needed operation. There is no need to provide access to particular part to user so each part could be accessed via enumerator. 
```cs
yield return new Rectangle(x, y, part.Width, height);
```
To provide access by "by operations" or "by parts" was added 2 functions:
```cs
public void ProcessByOperations(IEnumerable<IOperation> operations)
{
    foreach (var operation in operations)
    {
        foreach (var part in SplitImage(m_img.Size, m_processingSize))
        {
            operation.Execute(part);
        }
    }
}

public void ProcessByParts(IEnumerable<IOperation> operations)
{
    foreach (var part in SplitImage(m_img.Size, m_processingSize))
    {
        foreach (var operation in operations)
        {
            operation.Execute(part);
        }
    }
}
```
Operation executes for each ProcessingPart that have access to image raw data. This solution is fine for Grayscale operation but will be not fine for example for Flip or Rotate operation. The other possible way is to provide part as a System.Drowing.Bitmap. It will be easy perform any operation, but this solution:
* Memory consuming becase every time new System.Drowing.Bitmap will be created;
* Hard to merge changed bitmap (for example after grayscale) to origin.


# Ways to improve
Current solution is really bad. It uses System.Drawing.Bitmap inside. So immediatelly after loading sample image it consumes almost 1Gb of memory. It's defenetely not a good memory-optimized solution.

Possible way to solve it - using FileStream with Memory Mapped Files. But it also have proc and cons:
* Good performance;
* Will not use a lot of memory;
* Need to decode header manually or use one of BitmapDecoder;
* Need to have good knowledge about image structure to correctly read and save it;
* Need to use one of BitmapEncoder to properly save pieces of changed data (for splitting to files operation);

Also using of unsafe code for operating with raw image data could speed it up a bit.
