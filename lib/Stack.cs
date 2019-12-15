using System;

namespace LinkedList
{
    internal class  Stack 
    {
        private int _head;
        private int[] _values; 
        public Stack(){
            _values = new int[256];
        }

        public void Push(int value){
            if (_head==_values.LongLength){
                Reallocate();
            }
            _values[_head] = value;
            _head++;
        }

        private void Reallocate()
        {
            var values = new int[_head*2];
            _values.CopyTo(values,0);
            _values = values;

        }

        public bool TryPop(out int result){
            result = -1;
            if (_head>0){
                _head--;
                result = _values[_head];
                return true;
            }  
            return false;         
        } 


    }
}