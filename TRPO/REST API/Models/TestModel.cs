using System;
using System.Collections.Generic;
using System.Linq;

namespace REST_API.Models
{
    public class TestModel
    {
        private List<TestData> _dataList = new List<TestData>();
        private long _nextId = 1;

        public IEnumerable<TestData> GetAll() => _dataList;
        public TestData Get(int id) => _dataList.Find(p => p.Id == id);
        public void Remove(int id) => _dataList.RemoveAll(p => p.Id == id);

        public void Add(TestData item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            item.Id = _nextId++;
            _dataList.Add(item);
        }

        public bool Update(TestData item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            int index = _dataList.FindIndex(p => p.Id == item.Id);
            if (index == -1)
                return false;

            _dataList[index] = item;
            return true;
        }
    }

    public class TestData
    {
        public long Id { get; set; }
        public string TestString { get; set; }
        public float TestFloat { get; set; }
    }
}
