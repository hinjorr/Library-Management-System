using masood_lab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace masood_lab.Patterns
{
    
    class DBsingleton
    {
        protected static Library_systemEntities1 lib = new Library_systemEntities1();

        private DBsingleton() { }
        private static DBsingleton _object;
        public static DBsingleton getobject()
        {
            if (_object == null)
            {   
                _object = new DBsingleton();
            }
            return _object;
        }


        //books
        public static IList<SelectListItem> bookscategory()
        {
            Library_systemEntities1 lib = new Library_systemEntities1();
            IList<SelectListItem> data = (from q in lib.tblBooksCategories
                                          select new SelectListItem
                                          {
                                              Text = q.CategoryName,
                                              Value = q.ID.ToString(),
                                              Selected = false
                                          }).ToList();
            return data;
        }
        public bool AddBook(BooksModel books)
        {

            tblNewBooks_registration data = new tblNewBooks_registration()
            {
                BookName = books.BookName,
                BookAuthor = books.BookAuthor,
                BookPublisher = books.BookPublisher,
                BookPrice = books.BookPrice,
                BookCategory = books.BooksCategoryID,
                ImagePath = books.ImagePath
            };
            lib.tblNewBooks_registration.Add(data);
            try
            {
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }
        public List<BooksModel> ViewBooks()
        {
            List<BooksModel> data = (from books in lib.tblNewBooks_registration
                                     select new BooksModel
                                     {
                                         BookID = books.BookID,
                                         BookName = books.BookName,
                                         BookAuthor = books.BookAuthor,
                                         BookPublisher = books.BookPublisher,
                                         BookPrice = books.BookPrice,
                                         BooksCategory = books.tblBooksCategory.CategoryName
                                     }).ToList();
            return data;
        }
        public int nextid(BooksModel books)
        {
            try
            {
                int data = lib.tblNewBooks_registration.Max(x => x.BookID) + 1;
                books.BookID = data;
                return data;
            }
            catch (Exception)
            {
                int data = 0;
                return data;
            }

        }
        public BooksModel GetOneBook(int Fid)
        {
            var data = (from books in lib.tblNewBooks_registration
                        where books.BookID == Fid
                        select new BooksModel
                        {
                            BookID = books.BookID,
                            BookName = books.BookName,
                            BookAuthor = books.BookAuthor,
                            BookPublisher = books.BookPublisher,
                            BookPrice = books.BookPrice,
                            BooksCategoryID = books.BookCategory,
                            BooksCategory = books.tblBooksCategory.CategoryName,
                            ImagePath = books.ImagePath
                        }).FirstOrDefault();
            return data;
        }
        public bool UpdateBook(BooksModel books)
        {
            try
            {
                var data = lib.tblNewBooks_registration.Where(x => x.BookID == books.BookID).FirstOrDefault();
                data.BookID = books.BookID;
                data.BookName = books.BookName;
                data.BookAuthor = books.BookAuthor;
                data.BookPublisher = books.BookPublisher;
                data.BookPrice = books.BookPrice;
                data.BookCategory = books.BooksCategoryID;
                data.ImagePath = books.ImagePath;
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteBook(int Fid)
        {
            var data = lib.tblNewBooks_registration.Where(x => x.BookID == Fid).FirstOrDefault();
            lib.tblNewBooks_registration.Remove(data);
            try
            {
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Librarian
        public bool AddLibrarian(LibrarianModel Librarian)
        {

            tbl_librarian data = new tbl_librarian()
            {
                lib_id = Librarian.lib_id,
                lib_username = Librarian.lib_username,
                lib_password = Librarian.lib_password,
                Name = Librarian.Name,
                Email = Librarian.Email,
                Phonenumber = Librarian.Phonenumber,
                NIC = Librarian.NIC,
                ImagePath = Librarian.ImagePath,
                repeat_password = Librarian.repeat_password
            };
            var dt = lib.tbl_librarian.Where(x => x.lib_username == Librarian.lib_username).FirstOrDefault();
            if (dt != null)
            {
                return false;
            }
            else
            {
                lib.tbl_librarian.Add(data);
                lib.SaveChanges();
                tblLogin login = new tblLogin()
                {
                    lib_id = data.lib_id,
                    log_pasword = data.lib_password
                };
                lib.tblLogins.Add(login);
                lib.SaveChanges();
                return true;
            }


        }
        public int[] getnextid(LibrarianModel Librarian)
        {
            int[] values = new int[2];
            try
            {

                int lib_id = lib.tbl_librarian.Max(x => x.lib_id) + 1;
                Librarian.lib_id = lib_id;

                Librarian.logininfo = new LoginModel() { Loginid = lib.tblLogins.Max(x => x.Loginid) + 1 };


                values[0] = lib_id;
                values[1] = Librarian.logininfo.Loginid;
                return values;

            }
            catch (Exception)
            {
                values[0] = 0;
                values[1] = 0;
                return values;

            }


        }
        public List<LibrarianModel> ViewLibrarian()
        {
            List<LibrarianModel> Librarian = (from q in lib.tbl_librarian
                                              select new LibrarianModel
                                              {
                                                  lib_id = q.lib_id,
                                                  lib_username = q.lib_username,
                                                  lib_password = q.lib_password,
                                                  Name = q.Name,
                                                  Email = q.Email,
                                                  Phonenumber = q.Phonenumber,
                                                  NIC = q.NIC,
                                                  ImagePath = q.ImagePath,
                                                  repeat_password = q.repeat_password
                                              }).ToList();
            return Librarian;
        }
        public LibrarianModel GetOneLibrarian(int Fid)
        {
            var logindata = (from q in lib.tblLogins
                             where q.lib_id == Fid
                             select new MemberModel
                             {
                                 logininfo = new LoginModel()
                                 {
                                     Loginid = q.Loginid
                                 }
                             }).FirstOrDefault();

            var data = (from q in lib.tbl_librarian
                        where q.lib_id == Fid
                        select new LibrarianModel
                        {
                            lib_id = q.lib_id,
                            lib_username = q.lib_username,
                            lib_password = q.lib_password,
                            Name = q.Name,
                            Email = q.Email,
                            Phonenumber = q.Phonenumber,
                            NIC = q.NIC,
                            ImagePath = q.ImagePath,
                            repeat_password = q.repeat_password,
                            logininfo = new LoginModel() { Loginid = logindata.logininfo.Loginid }
                        }).FirstOrDefault();
            return data;
        }
        public bool UpdateLibrarian(LibrarianModel Librarian)
        {

            var data = lib.tbl_librarian.Where(m => m.lib_id == Librarian.lib_id).FirstOrDefault();
            data.Name = Librarian.Name;
            data.lib_password = Librarian.lib_password;
            data.Name = Librarian.Name;
            data.Email = Librarian.Email;
            data.Phonenumber = Librarian.Phonenumber;
            data.NIC = Librarian.NIC;
            data.ImagePath = Librarian.ImagePath;
            data.repeat_password = Librarian.repeat_password;
            try
            {
                lib.SaveChanges();
                var login = lib.tblLogins.Where(x => x.Loginid == Librarian.logininfo.Loginid).FirstOrDefault();
                login.log_pasword = Librarian.lib_password;
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool DeleteLibrarian(int Fid)
        {
            var data = lib.tbl_librarian.Where(x => x.lib_id == Fid).FirstOrDefault();
            lib.tbl_librarian.Remove(data);
            try
            {
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }


        //librarian
        public bool LoginFunc(LoginModel logindata)
        {
            var login = lib.tblLogins.Where(x => x.Loginid == logindata.Loginid && x.log_pasword == logindata.login_password).FirstOrDefault();
            if (login != null)
            {
                if (loginInfo(logindata))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }
        public bool loginInfo(LoginModel logindata)
        {
            var login = lib.tblLogins.Where(x => x.Loginid == logindata.Loginid && x.log_pasword == logindata.login_password).FirstOrDefault();
            if (login.memberID != null)
            {
                var memberDB = lib.tblNewMembers_registration.Where(x => x.memberID == login.memberID).FirstOrDefault();
                logindata.id = memberDB.memberID;
                logindata.Name = memberDB.FullName;
                logindata.ImagePath = memberDB.ImagePath;
                logindata.identification = "member";
                return true;
            }
            else if (login.lib_id != null)
            {
                var LibDB = lib.tbl_librarian.Where(x => x.lib_id == login.lib_id).FirstOrDefault();
                logindata.id = LibDB.lib_id;
                logindata.Name = LibDB.Name;
                logindata.ImagePath = LibDB.ImagePath;
                logindata.identification = "librarian";
                return true;
            }
            else if (login.admin_id != null)
            {
                var adminDB = lib.tbl_admin.Where(x => x.admin_id == login.admin_id).FirstOrDefault();
                logindata.id = adminDB.admin_id;
                logindata.Name = adminDB.admin_Name;
                logindata.ImagePath = adminDB.ImagePath;
                logindata.identification = "admin";
                return true;
            }
            else
            {
                return false;
            }

        }

        //member
        public bool AddMember(MemberModel member)
        {

            tblNewMembers_registration data = new tblNewMembers_registration()
            {
                FullName = member.Fullname,
                member_username = member.member_username,
                Phonenumber = member.Phonenumber,
                Address = member.Address,
                Gender = member.Gender,
                member_Password = member.Password,
                RepeatPassword = member.RepeatPassword,
                Email = member.Email,
                NIC = member.NIC,
                Date_of_birth = member.Date_of_birth,
                ImagePath = member.ImagePath,
            };


            var dt = lib.tblNewMembers_registration.Where(x => x.member_username == member.member_username).FirstOrDefault();
            if (dt != null)
            {
                return false;
            }
            else
            {
                lib.tblNewMembers_registration.Add(data);
                lib.SaveChanges();
                data.memberID = lib.tblNewMembers_registration.Max(x => x.memberID);
                tblLogin login = new tblLogin()
                {
                    memberID = data.memberID,
                    log_pasword = data.member_Password
                };
                lib.tblLogins.Add(login);
                lib.SaveChanges();
                return true;
            }

        }
        public MemberModel getnextid()
        {
            MemberModel member = new MemberModel();
            member.memberID = lib.tblNewMembers_registration.Max(x => x.memberID) + 1;
            member.loginid_test = lib.tblLogins.Max(x => x.Loginid) + 1;
            return member;
        }
        public IList<MemberModel> Viewmember()
        {
            try
            {
                Library_systemEntities1 lib = new Library_systemEntities1();
                IList<MemberModel> members = (from q in lib.tblNewMembers_registration
                                              select new MemberModel
                                              {
                                                  memberID = q.memberID,
                                                  member_username = q.member_username,
                                                  Fullname = q.FullName,
                                                  Phonenumber = q.Phonenumber,
                                                  NIC = q.NIC,
                                                  Password = q.member_Password,
                                                  RepeatPassword = q.RepeatPassword,
                                                  Email = q.Email,
                                                  Date_of_birth = q.Date_of_birth,
                                                  Gender = q.Gender,
                                                  Address = q.Address
                                              }).ToList();
                return members;
            }
            catch (Exception e)
            {

                throw;
            }


        }
        public MemberModel GetOneMember(int Fid)
        {
            var logindata = (from q in lib.tblLogins
                             where q.memberID == Fid
                             select new MemberModel
                             {
                                 logininfo = new LoginModel()
                                 {
                                     Loginid = q.Loginid
                                 }
                             }).FirstOrDefault();

            var member_data = (from q in lib.tblNewMembers_registration
                               where q.memberID == Fid
                               select new MemberModel
                               {
                                   memberID = q.memberID,
                                   member_username = q.member_username,
                                   Fullname = q.FullName,
                                   Phonenumber = q.Phonenumber,
                                   NIC = q.NIC,
                                   Email = q.Email,
                                   Address = q.Address,
                                   Password = q.member_Password,
                                   RepeatPassword = q.RepeatPassword,
                                   Date_of_birth = q.Date_of_birth,
                                   Gender = q.Gender,
                                   ImagePath = q.ImagePath,
                                   logininfo = new LoginModel() { Loginid = logindata.logininfo.Loginid }

                               }).FirstOrDefault();


            //MemberModel[] memberarray=new MemberModel[]{member_data,login_data};
            return member_data;

        }
        public bool UpdateMember(MemberModel member)
        {
            bool chk = false;
            var data = lib.tblNewMembers_registration.Where(m => m.memberID == member.memberID).FirstOrDefault();
            data.FullName = member.Fullname;
            data.Phonenumber = member.Phonenumber;
            data.NIC = member.NIC;
            data.Address = member.Address;
            data.Email = member.Email;
            data.Date_of_birth = member.Date_of_birth;
            data.member_Password = member.Password;
            data.RepeatPassword = member.RepeatPassword;
            data.Gender = member.Gender;
            data.ImagePath = member.ImagePath;
            try
            {
                lib.SaveChanges();
                var login = lib.tblLogins.Where(x => x.Loginid == member.logininfo.Loginid).FirstOrDefault();
                login.log_pasword = member.Password;
                lib.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool DeleteMember(int Fid)
        {
            var data = lib.tblNewMembers_registration.Where(x => x.memberID == Fid).FirstOrDefault();
            bool chk = false;

            lib.tblNewMembers_registration.Remove(data);
            try
            {
                lib.SaveChanges();
                return chk = true;
            }
            catch (Exception)
            {

                return chk = false;
            }


        }

        //Registration
        public int getid(RegistrationModel model)
        {
            try
            {
                int data = lib.tblBooksMembers.Max(x => x.registrationID) + 1;
                model.registrationID = data;
                return data;
            }
            catch (Exception)
            {

                int data = 0;
                return data;
            }


        }
        public void RegisterBook(RegistrationModel model)
        {
            tblBooksMember data = new tblBooksMember()
            {
                BookID = model.BooksID,
                MemberID = model.MemberID,
                IssuedDate = model.IssuedDate,
                ExpiryDate = model.ExpiryDate
            };
            lib.tblBooksMembers.Add(data);
            lib.SaveChanges();

        }
        public List<RegistrationModel> viewregistration()
        {
            List<RegistrationModel> registrations = (from q in lib.tblBooksMembers
                                                     select new RegistrationModel
                                                     {
                                                         registrationID = q.registrationID,
                                                         BookName = q.tblNewBooks_registration.BookName,
                                                         MemberName = q.tblNewMembers_registration.FullName,
                                                         IssuedDate = q.IssuedDate,
                                                         ExpiryDate = q.ExpiryDate
                                                     }).ToList();
            return registrations;

        }
        public RegistrationModel getonedata(int id)
        {
            var data = (from q in lib.tblBooksMembers
                        where q.registrationID == id
                        select new RegistrationModel
                        {
                            registrationID = q.registrationID,
                            BooksID = q.BookID,
                            MemberID = q.MemberID,
                            IssuedDate = q.IssuedDate,
                            ExpiryDate = q.ExpiryDate
                        }).FirstOrDefault();
            return data;

        }
        public bool update(RegistrationModel model)
        {
            var data = lib.tblBooksMembers.Where(x => x.registrationID == model.registrationID).FirstOrDefault();
            data.registrationID = model.registrationID;
            data.BookID = model.BooksID;
            data.MemberID = model.MemberID;
            data.IssuedDate = model.IssuedDate;
            data.ExpiryDate = model.ExpiryDate;
            lib.SaveChanges();
            return true;
        }
        public bool delete(int fid)
        {
            var data = lib.tblBooksMembers.Where(x => x.registrationID == fid).FirstOrDefault();
            lib.tblBooksMembers.Remove(data);
            bool chk = false;
            try
            {
                lib.SaveChanges();
                return chk = true;
            }
            catch (Exception ex)
            {

                return chk = false;
            }
        }



        //admin
        public bool AddAdmin(AdminModel Admin)
        {

            tbl_admin data = new tbl_admin()
            {
                admin_username = Admin.admin_username,
                admin_Name = Admin.admin_Name,
                admin_password = Admin.admin_password,
                ImagePath = Admin.ImagePath
            };

            var dt = lib.tbl_admin.Where(x => x.admin_username == Admin.admin_username).FirstOrDefault();
            if (dt != null)
            {
                return false;
            }
            else
            {
                lib.tbl_admin.Add(data);
                lib.SaveChanges();
                tblLogin login = new tblLogin()
                {
                    admin_id = data.admin_id,
                    log_pasword = data.admin_password
                };
                lib.tblLogins.Add(login);
                lib.SaveChanges();
                return true;
            }

        }
        public int[] getnextid(AdminModel Admin)
        {
            int[] values = new int[2];
            try
            {

                int admin_id = lib.tblNewMembers_registration.Max(x => x.memberID) + 1;
                Admin.admin_id = admin_id;

                Admin.logininfo = new LoginModel() { Loginid = lib.tblLogins.Max(x => x.Loginid) + 1 };


                values[0] = admin_id;
                values[1] = Admin.logininfo.Loginid;
                return values;

            }
            catch (Exception)
            {
                values[0] = 0;
                values[1] = 0;
                return values;

            }




            //end
        }

    }



}