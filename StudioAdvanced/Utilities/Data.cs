using System;
using System.Data;
using System.Data.SqlClient;
using StudioAdvanced.Models;

namespace StudioAdvanced.Utilities
{
    public class Data
    {
#if DEBUG
        private static readonly SqlConnection conn = new SqlConnection(@"Data Source=Gandalf;Initial Catalog=StudioAdvanced;User id='scott';Password=Morgan7509!");
#else
        private static readonly SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=StudioAdvanced;Integrated Security=True");
#endif
        private SqlCommand getAllFamiliesCmd = new SqlCommand("up_GetAllFamilies", conn);
        private SqlCommand getFamilyCmd = new SqlCommand("up_GetFamily", conn);
        private SqlCommand addFamilyCmd = new SqlCommand("up_AddFamily", conn);
        private SqlCommand updateFamilyCmd = new SqlCommand("up_UpdateFamily", conn);
        private SqlCommand deleteFamilyCmd = new SqlCommand("up_DeleteFamily", conn);
        private SqlCommand getAllStudentsCmd = new SqlCommand("up_GetAllStudents", conn);
        private SqlCommand getStudentCmd = new SqlCommand("up_GetStudent", conn);
        private SqlCommand addStudentCmd = new SqlCommand("up_AddStudent", conn);
        private SqlCommand updateStudentCmd = new SqlCommand("up_UpdateStudent", conn);
        private SqlCommand deleteStudentCmd = new SqlCommand("up_DeleteStudent", conn);
        private SqlCommand getAllClassesCmd = new SqlCommand("up_GetAllClasses", conn);
        private SqlCommand getClassCmd = new SqlCommand("up_GetClass", conn);
        private SqlCommand addClassCmd = new SqlCommand("up_AddClass", conn);
        private SqlCommand updateClassCmd = new SqlCommand("up_UpdateClass", conn);
        private SqlCommand deleteClassCmd = new SqlCommand("up_DeleteClass", conn);
        private SqlCommand getAllTeachersCmd = new SqlCommand("up_GetAllTeachers", conn);
        private SqlCommand getTeacherCmd = new SqlCommand("up_GetTeacher", conn);
        private SqlCommand addTeacherCmd = new SqlCommand("up_AddTeacher", conn);
        private SqlCommand updateTeacherCmd = new SqlCommand("up_UpdateTeacher", conn);
        private SqlCommand deleteTeacherCmd = new SqlCommand("up_DeleteTeacher", conn);
        private SqlCommand getSysParamsCmd = new SqlCommand("up_GetOptions", conn);
        private SqlCommand editSysParamsCmd = new SqlCommand("up_AddOption", conn);
        private SqlCommand printDaysheetCmd = new SqlCommand("up_PrintDaysheet", conn);
        private SqlCommand printClassRosterCmd = new SqlCommand("up_PrintClassRoster", conn);
        private SqlCommand printTeacherScheduleCmd = new SqlCommand("up_PrintTeacherSchedule", conn);
        private SqlCommand printAssistantScheduleCmd = new SqlCommand("up_PrintAssistantSchedule", conn);
        private SqlCommand printStudentScheduleCmd = new SqlCommand("up_PrintStudentSchedule", conn);
        private SqlCommand printStatementCmd = new SqlCommand("up_PrintStatement", conn);
        private SqlCommand printMonthlyCalendarCmd = new SqlCommand("up_PrintMonthlyCalendar", conn);
        private SqlCommand printPaymentReceiptCmd = new SqlCommand("up_PrintPaymentReceipt", conn);
        private SqlCommand addRegistrationCmd = new SqlCommand("up_AddRegistration", conn);
        private SqlCommand deleteRegistrationCmd = new SqlCommand("up_DeleteRegistration", conn);
        private SqlCommand applyChargeCmd = new SqlCommand("up_ApplyCharge", conn);
        private SqlCommand applyPaymentCmd = new SqlCommand("up_ApplyPayment", conn);
        private SqlCommand applyCreditCmd = new SqlCommand("up_ApplyCredit", conn);
        private SqlCommand addFundraisingCmd = new SqlCommand("up_AddFundraising", conn);
        private SqlCommand getFamilyNamesCmd = new SqlCommand("up_GetAllFamilyNames", conn);
        private SqlCommand getTeacherNamesCmd = new SqlCommand("up_GetTeacherNames", conn);
        private SqlCommand getClassroomsCmd = new SqlCommand("up_GetClassrooms", conn);
        private SqlCommand getClassLevelsCmd = new SqlCommand("up_GetClassLevels", conn);
        private SqlCommand getPayTypesCmd = new SqlCommand("up_GetPayTypes", conn);
        private SqlCommand getTransTypesCmd = new SqlCommand("up_GetTransTypes", conn);
        private SqlCommand editTransTypesCmd = new SqlCommand("up_EditTransTypes", conn);
        private SqlCommand deleteTransTypeCmd = new SqlCommand("up_DeleteTransType", conn);
        private SqlCommand editPayTypesCmd = new SqlCommand("up_EditPayTypes", conn);
        private SqlCommand deletePayTypeCmd = new SqlCommand("up_DeletePayType", conn);
        private SqlCommand deleteSysParamCmd = new SqlCommand("up_DeleteSysParam", conn);
        private SqlCommand getWeekDaysCmd = new SqlCommand("up_GetWeekDays", conn);
        private SqlCommand updateTuitionCmd = new SqlCommand("up_UpdateTuition", conn);
        private SqlCommand getTuitionCmd = new SqlCommand("up_GetTuition", conn);
        private SqlCommand addRecitalFeeCmd = new SqlCommand("up_AddRecitalFee", conn);
        private SqlCommand addRegistrationFeeCmd = new SqlCommand("up_AddRegistrationFee", conn);
        private SqlCommand getEmailAddressCmd = new SqlCommand("up_GetEmailAddress", conn);
        private SqlCommand getEmailPasswordCmd = new SqlCommand("up_GetEmailPassword", conn);
        private SqlCommand getFamilyEmailCmd = new SqlCommand("up_GetFamilyEmail", conn);
        private SqlCommand getClassIDListCmd = new SqlCommand("up_GetClassIDList", conn);
        private SqlCommand yearEndCmd = new SqlCommand("up_YearEnd", conn);
        private SqlCommand updateAccountChargeCmd = new SqlCommand("up_UpdateAccountCharge", conn);
        private SqlCommand getAccountSummaryCmd = new SqlCommand("up_GetAccountSummary", conn);
        private static Data _data;
        private static object _lockObject = new object();

        private Data()
        {
            getFamilyCmd.CommandType = CommandType.StoredProcedure;
            getFamilyCmd.Parameters.Add("@ID", SqlDbType.Int);
            getStudentCmd.CommandType = CommandType.StoredProcedure;
            getStudentCmd.Parameters.Add("@ID", SqlDbType.Int);
            getTeacherCmd.CommandType = CommandType.StoredProcedure;
            getTeacherCmd.Parameters.Add("@TeacherID", SqlDbType.Int);
            getClassCmd.Parameters.Add("@ClassID", SqlDbType.Int);
            getClassCmd.CommandType = CommandType.StoredProcedure;
            printStatementCmd.Parameters.Add("@FamilyID", SqlDbType.Int);
            printStatementCmd.CommandType = CommandType.StoredProcedure;
            getAccountSummaryCmd.Parameters.Add("@FamilyID", SqlDbType.Int);
            getAccountSummaryCmd.CommandType = CommandType.StoredProcedure;
        }

        public static Data Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_data == null)
                    {
                        _data = new Data();
                    }
                    return _data;
                }
            }
        }

        public SqlConnection Connection => conn;

        public DataSet GetWeekDays()
        {
            var sa = new SqlDataAdapter(getWeekDaysCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }
        public DataSet GetPayTypes()
        {
            var sa = new SqlDataAdapter(getPayTypesCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }
        public DataSet GetFamilyNames()
        {
            var sa = new SqlDataAdapter(getFamilyNamesCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }
        public DataSet GetFamilies()
        {
            var sa = new SqlDataAdapter(getAllFamiliesCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Families");
            return ds;
        }

        public DataSet GetFamily(int familyId)
        {
            getFamilyCmd.Parameters["@ID"].Value = familyId;
            var sa = new SqlDataAdapter(getFamilyCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }

        public bool AddFamily(FamilyModel family)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addFamilyCmd.Parameters.Clear();
                addFamilyCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 500).Value = family.FirstName;
                addFamilyCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = family.LastName;
                addFamilyCmd.Parameters.Add("@Address", SqlDbType.VarChar, 1000).Value = family.Address;
                addFamilyCmd.Parameters.Add("@City", SqlDbType.VarChar, 200).Value = family.City;
                addFamilyCmd.Parameters.Add("@State", SqlDbType.VarChar, 200).Value = family.State;
                addFamilyCmd.Parameters.Add("@PostalCode", SqlDbType.VarChar, 12).Value = family.ZipCode;
                addFamilyCmd.Parameters.Add("@PrimaryPhone", SqlDbType.VarChar, 45).Value = family.PrimaryPhone;
                addFamilyCmd.Parameters.Add("@SecondaryPhone", SqlDbType.VarChar, 45).Value = family.SecondaryPhone;
                addFamilyCmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 200).Value = family.EmailAddress;
                addFamilyCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addFamilyCmd.CommandType = CommandType.StoredProcedure;
                addFamilyCmd.ExecuteNonQuery();

                if ((int)addFamilyCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateFamily(FamilyModel family)
        {
            //var success = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                updateFamilyCmd.Parameters.Clear();
                updateFamilyCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = family.FamilyId;
                updateFamilyCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 500).Value = family.FirstName;
                updateFamilyCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = family.LastName;
                updateFamilyCmd.Parameters.Add("@Address", SqlDbType.VarChar, 1000).Value = family.Address;
                updateFamilyCmd.Parameters.Add("@City", SqlDbType.VarChar, 200).Value = family.City;
                updateFamilyCmd.Parameters.Add("@State", SqlDbType.VarChar, 200).Value = family.State;
                updateFamilyCmd.Parameters.Add("@PostalCode", SqlDbType.VarChar, 12).Value = family.ZipCode;
                updateFamilyCmd.Parameters.Add("@PrimaryPhone", SqlDbType.VarChar, 45).Value = family.PrimaryPhone;
                updateFamilyCmd.Parameters.Add("@SecondaryPhone", SqlDbType.VarChar, 45).Value = family.SecondaryPhone;
                updateFamilyCmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = family.EmailAddress;
                updateFamilyCmd.CommandType = CommandType.StoredProcedure;
                updateFamilyCmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteFamily(int familyId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteFamilyCmd.Parameters.Clear();
                deleteFamilyCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                deleteFamilyCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteFamilyCmd.CommandType = CommandType.StoredProcedure;
                deleteFamilyCmd.ExecuteNonQuery();

                if ((int)deleteFamilyCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet GetStudents()
        {
            var sa = new SqlDataAdapter(getAllStudentsCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Students");
            return ds;
        }

        public DataSet GetStudent(int studentId)
        {
            getStudentCmd.Parameters["@ID"].Value = studentId;
            var sa = new SqlDataAdapter(getStudentCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Student");
            return ds;
        }

        public bool AddStudent(StudentModel student)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addStudentCmd.Parameters.Clear();
                addStudentCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = student.FamilyId;
                addStudentCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 500).Value = student.FirstName;
                addStudentCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = student.LastName;
                addStudentCmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = student.Birthday;
                addStudentCmd.Parameters.Add("@Competition", SqlDbType.Bit).Value = student.Competition;
                addStudentCmd.Parameters.Add("@Assistant", SqlDbType.Bit).Value = student.IsAssistant;
                addStudentCmd.Parameters.Add("MBDiscount", SqlDbType.Bit).Value = student.MBDiscount;
                addStudentCmd.Parameters.Add("@Notes", SqlDbType.VarChar, 5000).Value = student.Notes;
                addStudentCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addStudentCmd.CommandType = CommandType.StoredProcedure;
                addStudentCmd.ExecuteNonQuery();

                if ((int)addStudentCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateStudent(StudentModel student)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                updateStudentCmd.Parameters.Clear();
                updateStudentCmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = student.StudentId;
                updateStudentCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 500).Value = student.FirstName;
                updateStudentCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = student.LastName;
                updateStudentCmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = student.Birthday;
                updateStudentCmd.Parameters.Add("@Competition", SqlDbType.Bit).Value = student.Competition;
                updateStudentCmd.Parameters.Add("@Assistant", SqlDbType.Bit).Value = student.IsAssistant;
                updateStudentCmd.Parameters.Add("MBDiscount", SqlDbType.Bit).Value = student.MBDiscount;
                updateStudentCmd.Parameters.Add("@Notes", SqlDbType.VarChar, 5000).Value = student.Notes;
                updateStudentCmd.Parameters.Add("@AssistantCredit", SqlDbType.Int).Value = student.AssistantCredit;
                updateStudentCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                updateStudentCmd.CommandType = CommandType.StoredProcedure;
                updateStudentCmd.ExecuteNonQuery();

                if ((int)updateStudentCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteStudent(int studentId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteStudentCmd.Parameters.Clear();
                deleteStudentCmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                deleteStudentCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteStudentCmd.CommandType = CommandType.StoredProcedure;
                deleteStudentCmd.ExecuteNonQuery();

                if ((int)deleteStudentCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet GetClassIDList()
        {
            var sa = new SqlDataAdapter(getClassIDListCmd);
            var ds = new DataSet();
            sa.Fill(ds, "ClassIDs");
            return ds;
        }

        public DataSet GetAllClasses()
        {
            var sa = new SqlDataAdapter(getAllClassesCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Classes");
            return ds;
        }

        public DataSet GetClass(int classId)
        {
            getClassCmd.Parameters["@ClassID"].Value = classId;
            var sa = new SqlDataAdapter(getClassCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Class");
            return ds;
        }

        public bool AddClass(ClassModel newClass)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addClassCmd.Parameters.Clear();
                addClassCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 500).Value = newClass.ClassName;
                addClassCmd.Parameters.Add("@ClassDay", SqlDbType.Int).Value = newClass.DayOfWeek;
                addClassCmd.Parameters.Add("@ClassTime", SqlDbType.VarChar, 50).Value = newClass.ClassTime;
                addClassCmd.Parameters.Add("@ClassRoutine", SqlDbType.VarChar, 500).Value = newClass.RoutineName;
                addClassCmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = newClass.TeacherId;
                addClassCmd.Parameters.Add("@ClassroomID", SqlDbType.Int).Value = newClass.ClassroomId;
                addClassCmd.Parameters.Add("@ClassLevelID", SqlDbType.Int).Value = newClass.ClassLevelID;
                addClassCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addClassCmd.CommandType = CommandType.StoredProcedure;
                addClassCmd.ExecuteNonQuery();
                if ((int)addClassCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateClass(ClassModel updClass)
        {
            {
                var success = false;

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    updateClassCmd.Parameters.Clear();
                    updateClassCmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = updClass.ClassId;
                    updateClassCmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 500).Value = updClass.ClassName;
                    updateClassCmd.Parameters.Add("@ClassDay", SqlDbType.Int).Value = updClass.DayOfWeek;
                    updateClassCmd.Parameters.Add("@ClassTime", SqlDbType.VarChar, 25).Value = updClass.ClassTime;
                    updateClassCmd.Parameters.Add("@ClassRoutine", SqlDbType.VarChar, 500).Value = updClass.RoutineName;
                    updateClassCmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = updClass.TeacherId;
                    updateClassCmd.Parameters.Add("@ClassroomID", SqlDbType.Int).Value = updClass.ClassroomId;
                    updateClassCmd.Parameters.Add("@ClassLevelID", SqlDbType.Int).Value = updClass.ClassLevelID;
                    updateClassCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    updateClassCmd.CommandType = CommandType.StoredProcedure;
                    updateClassCmd.ExecuteNonQuery();
                    if ((int)updateClassCmd.Parameters["@ReturnCode"].Value == 0)
                    {
                        success = true;
                    }
                    conn.Close();
                    return success;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public bool DeleteClass(int classId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteClassCmd.Parameters.Clear();
                deleteClassCmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = classId;
                deleteClassCmd.Parameters.Add("ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteClassCmd.CommandType = CommandType.StoredProcedure;
                deleteClassCmd.ExecuteNonQuery();

                if ((int) deleteClassCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet GetAllTeachers()
        {
            var sa = new SqlDataAdapter(getAllTeachersCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Teachers");
            return ds;
        }

        public DataSet GetTeacher(int teacherId)
        {
            getTeacherCmd.Parameters["@TeacherID"].Value = teacherId;
            var sa = new SqlDataAdapter(getTeacherCmd);
            var ds = new DataSet();
            sa.Fill(ds, "Teacher");
            return ds;
        }

        public bool AddTeacher(TeacherModel newTeacher)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addTeacherCmd.Parameters.Clear();
                addTeacherCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = newTeacher.FirstName;
                addTeacherCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = newTeacher.LastName;
                addTeacherCmd.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = newTeacher.Address;
                addTeacherCmd.Parameters.Add("@City", SqlDbType.VarChar, 300).Value = newTeacher.City;
                addTeacherCmd.Parameters.Add("@State", SqlDbType.VarChar, 5).Value = newTeacher.State;
                addTeacherCmd.Parameters.Add("@Postal", SqlDbType.VarChar, 25).Value = newTeacher.Postal;
                addTeacherCmd.Parameters.Add("@PrimaryPhone", SqlDbType.VarChar, 45).Value = newTeacher.PrimaryPhone;
                addTeacherCmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = newTeacher.Email;
                addTeacherCmd.Parameters.Add("@SecondaryPhone", SqlDbType.VarChar, 45).Value = newTeacher.SecondaryPhone;
                addTeacherCmd.Parameters.Add("@PayRate", SqlDbType.Decimal).Value = newTeacher.PayRate;
                addTeacherCmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = newTeacher.Birthday;
                addTeacherCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addTeacherCmd.CommandType = CommandType.StoredProcedure;
                addTeacherCmd.ExecuteNonQuery();

                if ((int) addTeacherCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateTeacher(TeacherModel updTeacher)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                updateTeacherCmd.Parameters.Clear();
                updateTeacherCmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = updTeacher.TeacherId;
                updateTeacherCmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = updTeacher.FirstName;
                updateTeacherCmd.Parameters.Add("@LastName", SqlDbType.VarChar, 500).Value = updTeacher.LastName;
                updateTeacherCmd.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = updTeacher.Address;
                updateTeacherCmd.Parameters.Add("@City", SqlDbType.VarChar, 300).Value = updTeacher.City;
                updateTeacherCmd.Parameters.Add("@State", SqlDbType.VarChar, 5).Value = updTeacher.State;
                updateTeacherCmd.Parameters.Add("@Postal", SqlDbType.VarChar, 25).Value = updTeacher.Postal;
                updateTeacherCmd.Parameters.Add("@PrimaryPhone", SqlDbType.VarChar, 45).Value = updTeacher.PrimaryPhone;
                updateTeacherCmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = updTeacher.Email;
                updateTeacherCmd.Parameters.Add("@SecondaryPhone", SqlDbType.VarChar, 45).Value = updTeacher.SecondaryPhone;
                updateTeacherCmd.Parameters.Add("@PayRate", SqlDbType.Decimal).Value = updTeacher.PayRate;
                updateTeacherCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                updateTeacherCmd.CommandType = CommandType.StoredProcedure;
                updateTeacherCmd.ExecuteNonQuery();

                if ((int)updateTeacherCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteTeacher(int teacherId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteTeacherCmd.Parameters.Clear();
                deleteTeacherCmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = teacherId;
                deleteTeacherCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteTeacherCmd.CommandType = CommandType.StoredProcedure;
                deleteTeacherCmd.ExecuteNonQuery();

                if ((int) deleteTeacherCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintDaySheet()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printDaysheetCmd.Parameters.Clear();
                printDaysheetCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printDaysheetCmd.CommandType = CommandType.StoredProcedure;
                printDaysheetCmd.ExecuteNonQuery();

                if ((int) printDaysheetCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet GetSysParams()
        {
            var sa = new SqlDataAdapter(getSysParamsCmd);
            var ds = new DataSet();
            sa.Fill(ds, "System Parameters");
            return ds;
        }

        public bool UpdateOptions(string optionName, string value)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                editSysParamsCmd.Parameters.Clear();
                editSysParamsCmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = optionName;
                editSysParamsCmd.Parameters.Add("@Value", SqlDbType.VarChar, 500).Value = value;
                editSysParamsCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                editSysParamsCmd.CommandType = CommandType.StoredProcedure;
                updateStudentCmd.ExecuteNonQuery();

                if ((int) editSysParamsCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintClassRoster()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printClassRosterCmd.Parameters.Clear();
                printClassRosterCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printClassRosterCmd.CommandType = CommandType.StoredProcedure;
                printClassRosterCmd.ExecuteNonQuery();

                if ((int)printClassRosterCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintTeacherSchedule()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printTeacherScheduleCmd.Parameters.Clear();
                printTeacherScheduleCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printTeacherScheduleCmd.CommandType = CommandType.StoredProcedure;
                printTeacherScheduleCmd.ExecuteNonQuery();

                if ((int)printTeacherScheduleCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintStudentSchedule()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printStudentScheduleCmd.Parameters.Clear();
                printStudentScheduleCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printStudentScheduleCmd.CommandType = CommandType.StoredProcedure;
                printStudentScheduleCmd.ExecuteNonQuery();

                if ((int)printStudentScheduleCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintAssistantSchedule()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printAssistantScheduleCmd.Parameters.Clear();
                printAssistantScheduleCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printAssistantScheduleCmd.CommandType = CommandType.StoredProcedure;
                printAssistantScheduleCmd.ExecuteNonQuery();

                if ((int)printAssistantScheduleCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet PrintStatement(int familyId)
        {
            printStatementCmd.Parameters["@FamilyID"].Value = familyId;
            var sa = new SqlDataAdapter(printStatementCmd);
            var ds = new DataSet();
            sa.Fill(ds);

            return ds;
        }

        public DataSet GetAccountSummary(int familyId)
        {
            getAccountSummaryCmd.Parameters["@FamilyID"].Value = familyId;
            var sa = new SqlDataAdapter(getAccountSummaryCmd);
            var ds = new DataSet();
            sa.Fill(ds);

            return ds;
        }

        public bool PrintPaymentReceipt()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printPaymentReceiptCmd.Parameters.Clear();
                printPaymentReceiptCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printPaymentReceiptCmd.CommandType = CommandType.StoredProcedure;
                printPaymentReceiptCmd.ExecuteNonQuery();

                if ((int)printPaymentReceiptCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool PrintMonthlyCalendar()
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                printMonthlyCalendarCmd.Parameters.Clear();
                printMonthlyCalendarCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                printMonthlyCalendarCmd.CommandType = CommandType.StoredProcedure;
                printMonthlyCalendarCmd.ExecuteNonQuery();

                if ((int)printMonthlyCalendarCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool AddRegistration(int studentId, int familyId, int classId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addRegistrationCmd.Parameters.Clear();
                addRegistrationCmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                addRegistrationCmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = classId;
                addRegistrationCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                addRegistrationCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addRegistrationCmd.CommandType = CommandType.StoredProcedure;
                addRegistrationCmd.ExecuteNonQuery();

                if ((int)addRegistrationCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteRegistration(int studentId, int classId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteRegistrationCmd.Parameters.Clear();
                deleteRegistrationCmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentId;
                deleteRegistrationCmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = classId;
                deleteRegistrationCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteRegistrationCmd.CommandType = CommandType.StoredProcedure;
                deleteRegistrationCmd.ExecuteNonQuery();

                if ((int)deleteRegistrationCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool ApplyCharge(int familyId, int transTypeId, decimal amountDue, string comments)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                applyChargeCmd.Parameters.Clear();
                applyChargeCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                applyChargeCmd.Parameters.Add("@TransTypeID", SqlDbType.Int).Value = transTypeId;
                applyChargeCmd.Parameters.Add("@AmountDue", SqlDbType.Money).Value = amountDue;
                applyChargeCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 1000).Value = comments;
                applyChargeCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                applyChargeCmd.CommandType = CommandType.StoredProcedure;
                applyChargeCmd.ExecuteNonQuery();

                if ((int)applyChargeCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool ApplyPayment(int familyId, decimal amountPaid, int payTypeId, string comments, DateTime payDate)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                applyPaymentCmd.Parameters.Clear();
                applyPaymentCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                applyPaymentCmd.Parameters.Add("@AmountPaid", SqlDbType.Money).Value = amountPaid;
                applyPaymentCmd.Parameters.Add("@PayTypeID", SqlDbType.Int).Value = payTypeId;
                applyPaymentCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 1000).Value = comments;
                applyPaymentCmd.Parameters.Add("@PayDate", SqlDbType.Date).Value = payDate;
                applyPaymentCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                applyPaymentCmd.CommandType = CommandType.StoredProcedure;
                applyPaymentCmd.ExecuteNonQuery();

                if ((int)applyPaymentCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public bool AddFundraising(int familyId, decimal fundraisingAmount, string comments)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                addFundraisingCmd.Parameters.Clear();
                addFundraisingCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                addFundraisingCmd.Parameters.Add("@FundraisingAmount", SqlDbType.Money).Value = fundraisingAmount;
                addFundraisingCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 1000).Value = comments;
                addFundraisingCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                addFundraisingCmd.CommandType = CommandType.StoredProcedure;
                addFundraisingCmd.ExecuteNonQuery();

                if ((int)addFundraisingCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public bool ApplyCredit(int familyId, decimal creditAmount, string comments)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                applyCreditCmd.Parameters.Clear();
                applyCreditCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                applyCreditCmd.Parameters.Add("@CreditAmount", SqlDbType.Money).Value = creditAmount;
                applyCreditCmd.Parameters.Add("@Comments", SqlDbType.VarChar, 1000).Value = comments;
                applyCreditCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                applyCreditCmd.CommandType = CommandType.StoredProcedure;
                applyCreditCmd.ExecuteNonQuery();

                if ((int)applyCreditCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataSet GetTeacherNames()
        {
            var sa = new SqlDataAdapter(getTeacherNamesCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }

        public DataSet GetClassrooms()
        {
            var sa = new SqlDataAdapter(getClassroomsCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }

        public DataSet GetClassLevels()
        {
            var sa = new SqlDataAdapter(getClassLevelsCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }

        public DataSet GetTransTypes()
        {
            var sa = new SqlDataAdapter(getTransTypesCmd);
            var ds = new DataSet();
            sa.Fill(ds);
            return ds;
        }

        public bool EditTransTypes(int transTypeId, string description)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                editTransTypesCmd.Parameters.Clear();
                editTransTypesCmd.Parameters.Add("@TransTypeID", SqlDbType.Int).Value = transTypeId;
                editTransTypesCmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = description;
                editTransTypesCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                editTransTypesCmd.CommandType = CommandType.StoredProcedure;
                editTransTypesCmd.ExecuteNonQuery();

                if ((int)editTransTypesCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteTransType(int transTypeId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteTransTypeCmd.Parameters.Clear();
                deleteTransTypeCmd.Parameters.Add("@TransTypeID", SqlDbType.Int).Value = transTypeId;
                deleteTransTypeCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteTransTypeCmd.CommandType = CommandType.StoredProcedure;
                deleteTransTypeCmd.ExecuteNonQuery();

                if ((int)deleteTransTypeCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool EditPayTypes(int payTypeId, string description)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                editPayTypesCmd.Parameters.Clear();
                editPayTypesCmd.Parameters.Add("@PayTypeID", SqlDbType.Int).Value = payTypeId;
                editPayTypesCmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = description;
                editPayTypesCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                editPayTypesCmd.CommandType = CommandType.StoredProcedure;
                editPayTypesCmd.ExecuteNonQuery();

                if ((int)editPayTypesCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeletePayType(int payTypeId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deletePayTypeCmd.Parameters.Clear();
                deletePayTypeCmd.Parameters.Add("@PayTypeID", SqlDbType.Int).Value = payTypeId;
                deletePayTypeCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deletePayTypeCmd.CommandType = CommandType.StoredProcedure;
                deletePayTypeCmd.ExecuteNonQuery();

                if ((int)deletePayTypeCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteSysParam(int parameterId)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                deleteSysParamCmd.Parameters.Clear();
                deleteSysParamCmd.Parameters.Add("@PayTypeID", SqlDbType.Int).Value = parameterId;
                deleteSysParamCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                deleteSysParamCmd.CommandType = CommandType.StoredProcedure;
                deleteSysParamCmd.ExecuteNonQuery();

                if ((int)deleteSysParamCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool EditSysParams(int parameterId, string description)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                editSysParamsCmd.Parameters.Clear();
                editSysParamsCmd.Parameters.Add("@ParameterID", SqlDbType.Int).Value = parameterId;
                editSysParamsCmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = description;
                editSysParamsCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                editSysParamsCmd.CommandType = CommandType.StoredProcedure;
                editSysParamsCmd.ExecuteNonQuery();

                if ((int)editSysParamsCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool UpdateTuition(int familyId, decimal tuition)
        {
            var success = false;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                updateTuitionCmd.Parameters.Clear();
                updateTuitionCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                updateTuitionCmd.Parameters.Add("@Tuition", SqlDbType.Money).Value = tuition;
                updateTuitionCmd.Parameters.Add("@ReturnCode", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                updateTuitionCmd.CommandType = CommandType.StoredProcedure;
                updateTuitionCmd.ExecuteNonQuery();

                if ((int)updateTuitionCmd.Parameters["@ReturnCode"].Value == 0)
                {
                    success = true;
                }
                conn.Close();
                return success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdateAccountCharge(int transID, decimal amountDue, string notes)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                updateAccountChargeCmd.Parameters.Clear();
                updateAccountChargeCmd.Parameters.Add("@TransID", SqlDbType.Int).Value = transID;
                updateAccountChargeCmd.Parameters.Add("@AmountDue", SqlDbType.Money).Value = amountDue;
                updateAccountChargeCmd.Parameters.Add("@Notes", SqlDbType.VarChar, 1000).Value = notes;
                updateAccountChargeCmd.CommandType = CommandType.StoredProcedure;
                updateAccountChargeCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetTuition(int familyId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                getTuitionCmd.Parameters.Clear();
                getTuitionCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                getTuitionCmd.CommandType = CommandType.StoredProcedure;
                var result = getTuitionCmd.ExecuteScalar() as string;
                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void YearEnd()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            yearEndCmd.Parameters.Clear();
            yearEndCmd.CommandType = CommandType.StoredProcedure;
            yearEndCmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddRecitalFee()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            addRecitalFeeCmd.Parameters.Clear();
            addRecitalFeeCmd.CommandType = CommandType.StoredProcedure;
            addRecitalFeeCmd.ExecuteNonQuery();
            conn.Close();
        }

        public void AddRegistrationFee()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            addRegistrationFeeCmd.Parameters.Clear();
            addRegistrationFeeCmd.CommandType = CommandType.StoredProcedure;
            addRegistrationFeeCmd.ExecuteNonQuery();
            conn.Close();
        }

        public string GetEmailAddress()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                getEmailAddressCmd.Parameters.Clear();
                getEmailAddressCmd.CommandType = CommandType.StoredProcedure;
                var result = getEmailAddressCmd.ExecuteScalar() as string;
                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetEmailPassword()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                getEmailPasswordCmd.CommandType = CommandType.StoredProcedure;
                var result = getEmailPasswordCmd.ExecuteScalar() as string;
                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetFamilyEmail(int familyId)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                getFamilyEmailCmd.Parameters.Clear();
                getFamilyEmailCmd.Parameters.Add("@FamilyID", SqlDbType.Int).Value = familyId;
                getFamilyEmailCmd.CommandType = CommandType.StoredProcedure;
                var result = getFamilyEmailCmd.ExecuteScalar() as string;
                conn.Close();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
