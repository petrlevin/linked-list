# linked-list

Here is implementation of doubly linked list that does store data at underlying array - each element of array has fields for index of next and previous node. Such design allows escape memory allocation at managed heap.  It could be usefull with huge collections of value type data with intention of frequent insertions.





## benchmark [source](bench/Insertions.cs)


|                                Method |       N |         Mean |       Error |     StdDev |      Gen 0 |     Gen 1 | Gen 2 |    Allocated |
|-------------------------------------- |-------- |-------------:|------------:|-----------:|-----------:|----------:|------:|-------------:|
|                            LinkedList |   65536 |   2,151.0 us |   105.20 us |   288.0 us |          - |         - |     - |   2048.09 KB |
| System.Collections.Generic.LinkedList |   65536 |     572.1 us |    42.67 us |   119.7 us |          - |         - |     - |       768 KB |
|                            LinkedList | 1048576 |  30,159.8 us |   165.24 us |   129.0 us |          - |         - |     - |  32768.09 KB |
| System.Collections.Generic.LinkedList | 1048576 |  25,987.6 us |   269.68 us |   239.1 us |  2000.0000 |         - |     - |     12288 KB |
|                            LinkedList | 8388608 | 258,439.0 us | 1,615.77 us | 1,511.4 us |          - |         - |     - | 262144.14 KB |
| System.Collections.Generic.LinkedList | 8388608 | 580,497.1 us | 4,601.77 us | 4,079.4 us | 16000.0000 | 5000.0000 |     - |  98304.05 KB |
