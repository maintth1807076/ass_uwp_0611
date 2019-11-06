using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ass_uwp_0611.Entity;
using ass_uwp_0611.Utils;
using SQLitePCL;

namespace ass_uwp_0611.Models
{
    class ContactModel
    {
        public bool Insert(ass_uwp_0611.Entity.Contact contact)
        {
            try
            {
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("INSERT INTO Contact (Name, PhoneNumber) VALUES (?, ?)"))
                {
                    stt.Bind(1, contact.Name);
                    stt.Bind(2, contact.PhoneNumber);
                    stt.Step();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public ObservableCollection<ass_uwp_0611.Entity.Contact> GetContactByName(string name)
        {
            try
            {
                var list = new ObservableCollection<ass_uwp_0611.Entity.Contact>();
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("SELECT * FROM Contact WHERE Name = ?"))
                {
                    stt.Bind(1, name);
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new ass_uwp_0611.Entity.Contact
                        {
                            Name = (string)stt[1],
                            PhoneNumber = (long)stt[2],
                        });
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ObservableCollection<ass_uwp_0611.Entity.Contact> GetListContacts()
        {
            try
            {
                var list = new ObservableCollection<ass_uwp_0611.Entity.Contact>();
                using (var stt = SQLiteUtil.GetIntances().Connection.Prepare("SELECT * FROM Contact"))
                {

                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        list.Add(new ass_uwp_0611.Entity.Contact
                        {
                            Name = (string)stt[1],
                            PhoneNumber = (long)stt[2],
                        });
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
