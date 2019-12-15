# linked-list

Here is implementation of doubly linked list that does store data at underlying array - each element of array has fields for index of next and previous node. Such design allows escape memory allocation at managed heap.  It could be usefull with huge collections of value type data with intention of frequent insertions.





## benchmark [source](bench/Insertions.cs)




|                                Method |       N |         Mean |        Error |      StdDev |      Gen 0 |     Gen 1 | Gen 2 |   Allocated |
|-------------------------------------- |-------- |-------------:|-------------:|------------:|-----------:|----------:|------:|------------:|
|                            LinkedList |   65536 |   1,294.9 us |     45.53 us |    126.9 us |          - |         - |     - |           - |
| System.Collections.Generic.LinkedList |   65536 |     607.7 us |     46.24 us |    126.6 us |          - |         - |     - |    786432 B |
|                            LinkedList | 1048576 |  18,569.8 us |    278.52 us |    260.5 us |          - |         - |     - |           - |
| System.Collections.Generic.LinkedList | 1048576 |  26,550.6 us |    505.23 us |    540.6 us |  2000.0000 |         - |     - |  12582912 B |
|                            LinkedList | 8388608 | 148,399.1 us |  1,282.85 us |  1,137.2 us |          - |         - |     - |        48 B |
| System.Collections.Generic.LinkedList | 8388608 | 588,508.4 us | 11,732.07 us | 18,265.4 us | 16000.0000 | 5000.0000 |     - | 100663344 B |
