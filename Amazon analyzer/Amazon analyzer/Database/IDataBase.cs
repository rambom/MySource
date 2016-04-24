using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;

namespace Amazon_analyzer.Database
{
    /// <summary>
    /// ���ݷ��ʽӿ�
    /// </summary>
    public interface IDataBase : IDisposable
    {
        /// <summary>
        /// ��ȡ��������ǰ׺
        /// </summary>
        /// <returns></returns>
        string GetDbPrefix();
        /// <summary>
        /// ��ȡ���ݿ�ƴ���ַ� SQL+ ORACLE||
        /// </summary>
        /// <returns></returns>
        string GetDbJoinString();

        /// <summary>
        /// ������
        /// </summary>
        void OpenTrans();

        /// <summary>
        /// �����ύ
        /// </summary>
        void CommitTrans();

        /// <summary>
        /// ����ع�
        /// </summary>
        void RollbackTrans();

        /// <summary>
        /// �ر�
        /// </summary>
        void CloseConn();

        /// <summary>
        /// ִ�зǲ�ѯ��SQL���
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>��Ӱ�������</returns>
        int ExecuteNoQuery(string strSql, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�в�ѯ��䷵��DataTable
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string strSql, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�в�ѯ��䷵��DataSet
        /// </summary>
        /// <param name="strSql">SQL���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteDataSet(string strSql, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�в�ѯ��䣬��ȡ��һ�е�һ�е�ֵ
        /// </summary>
        /// <param name="strSql">SQL��ѯ���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>��ѯ���</returns>
        dynamic ExecuteScalar(string strSql, List<DataParameter> arrParam);

        /// <summary>
        /// ִ��SQL��䷵��DataReader
        /// </summary>
        /// <param name="strSql">SQL��ѯ���</param>
        /// <param name="arrParam">��������</param>
        /// <returns>��ѯ���</returns>
        DbDataReader ExecuteDataReader(string strSql, List<DataParameter> arrParam);

        /// <summary>
        /// ��datatable���ݼ���������ļ��е����ݿ�
        /// </summary>
        /// <param name="dtImport">���ݼ�</param>
        /// <param name="strSql">sql��ѯ���</param>
        /// <param name="intStartRow">��ʼ��</param>
        void WriteDataTableToDB(DataTable dtImport, string strSql, int intStartRow);

        /// <summary>
        /// ��Adapter��ʽ����Datatableӳ����е����ݿ��
        /// </summary>
        /// <param name="dataTable">Ҫ�����dataTable</param>
        /// <param name="tableName">Ŀ�����</param>
        /// <param name="columnMapping">��ӳ���ϵ(��1��ΪԴ��,��2��ΪĿ����)</param>
        /// <returns>�ɹ�����success������Ϊ���ش�����Ϣ</returns>
        string ImportDatatableToDB(DataTable dataTable, string tableName, DataRow[] columnMapping);

        /// <summary>
        /// ���ٵ���dataTableӳ����������ݿ��(Ŀ���������ִ�Сд)
        /// </summary>
        /// <param name="dataTable">Ҫ�����dataTable</param>
        /// <param name="tableName">Ŀ�����</param>
        /// <param name="columnMapping">��ӳ���ϵ(��1��ΪԴ��,��2��ΪĿ����)</param>
        /// <returns>�ɹ�����success������Ϊ���ش�����Ϣ</returns>
        string FastImportDatatableToDB(DataTable dataTable, string tableName, DataRow[] columnMapping);

        /// <summary>
        /// ���ٵ���dataTableӳ��������������ݿ��(Ŀ���������ִ�Сд)
        /// </summary>
        /// <param name="dataTable">Ҫ�����dataTable</param>
        /// <param name="tableName">Ŀ�����</param>
        /// <param name="arrStrTarGetColumns">Ŀ������</param>
        /// <returns>�ɹ�����success������Ϊ���ش�����Ϣ</returns>
        string FastImportDatatableToDB(DataTable dataTable, string tableName, string[] arrStrTarGetColumns);

        /// <summary>
        /// ���ٵ���dataTableӳ��������������ݿ��(Ŀ���������ִ�Сд)
        /// </summary>
        /// <param name="dataTable">Ҫ�����dataTable</param>
        /// <param name="tableName">Ŀ�����</param>
        /// <param name="arrStrTarGetColumns">Ŀ������</param>
        /// <returns>�ɹ�����success������Ϊ���ش�����Ϣ</returns>
        string FastImportDatatableToDBNotClose(DataTable dataTable, string tableName, string[] arrStrTarGetColumns);
       
        /// <summary>
        /// ����DataReaderӳ����������ݿ�����������������ÿ���ύ����������ִ�е�ʱ��
        /// </summary>
        /// <param name="strConnectionString">Ŀ�����ݿ������ַ���</param>
        /// <param name="intTimeOut">�������ʱʱ��</param>
        /// <param name="dataReader">Ҫ�����DataReader</param>
        /// <param name="strTableName">Ŀ�����</param>
        /// <param name="columnMapping">��ӳ���ϵ(Ŀ����,Դ��)</param>
        /// <param name="intBatchSize">ÿ�ε�����������ύ</param>
        /// <param name="intCommitRowCount">����ɹ�����</param>
        /// <param name="spentTime">����ʱ��</param>
        /// <param name="strErrMsg">������Ϣ</param>
        /// <returns>ִ�н��</returns>
        bool FastImportDataReaderToDb(string strConnectionString, int intBatchSize, int intTimeOut, IDataReader dataReader,
                                      string strTableName,
                                      Dictionary<string, string> columnMapping, out int intCommitRowCount, out long spentTime, out string strErrMsg);

        /// <summary>
        /// ���ٵ���ӵڼ��п�ʼ���ı��ļ�ӳ����������ݿ�����������������ÿ���ύ����������ִ�е�ʱ�䡣
        /// </summary>
        /// <param name="strConnectionString">Ŀ�����ݿ������ַ���</param>
        /// <param name="intTimeOut">�������ʱʱ��</param>
        /// <param name="strTxtFile">Ҫ������ı��ļ�·��</param>
        /// <param name="strHead">��ͷ��ʽ���ָ���</param>
        /// <param name="strDelimited">�зָ���</param>
        /// <param name="strTableName">Ŀ�����</param>
        /// <param name="columnMapping">��ӳ���ϵ(Ŀ����,Դ��)</param>
        /// <param name="intBatchSize">ÿ�ε�����������ύ</param>
        /// <param name="extraMapping">������ӳ��</param>
        /// <param name="strRowNumberColumn">�����к�����(null:�������к�)</param>
        /// <param name="intCommitRowCount">����ɹ�����</param>
        /// <param name="lSpentTime">����ʱ��</param>
        /// <param name="strErrMsg">������Ϣ</param>
        /// <param name="intStartRow">�ӵ�X�п�ʼ����</param>
        /// <returns>ִ�н��</returns>
        bool FastImportTextToDb(string strConnectionString, int intBatchSize, int intTimeOut, string strTxtFile,
                                string strHead, string strDelimited, string strTableName,
                                Dictionary<string, string> columnMapping, Dictionary<string, object> extraMapping, uint intStartRow, string strRowNumberColumn,
                                out int intCommitRowCount, out long lSpentTime, out string strErrMsg);

        /// <summary>
        /// ִ�д洢���̷���DataSet
        /// </summary>
        /// <param name="proName">�洢����</param>
        /// <param name="arrParam">��������</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteDataSetSP(string proName, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�д洢���̷���Scalar
        /// </summary>
        /// <param name="proName">�洢����</param>
        /// <param name="arrParam">��������</param>
        /// <returns>Scalar</returns>
        dynamic ExecuteScalarSP(string proName, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�д洢���̷����������
        /// </summary>
        /// <param name="proName">�洢����</param>
        /// <param name="arrParam">��������</param>
        /// <returns>���������ֵ</returns>
        string ExecuteStringSP(string proName, List<DataParameter> arrParam);

        /// <summary>
        /// ִ�д洢�����޷���ֵ
        /// </summary>
        /// <param name="proName">�洢����</param>
        /// <param name="arrParam">��������</param>
        void ExecuteNoQuerySP(string proName, List<DataParameter> arrParam);

        /// <summary>
        /// ��ѯ��ȡ��ҳ�����
        /// </summary>
        /// <param name="pagedTableName">����</param>
        /// <param name="pagedSelectColumns">��</param>
        /// <param name="pagedPrimaryKey">����</param>
        /// <param name="whereCondition">����</param>
        /// <param name="arrParam">��������</param>
        /// <param name="pagedOrderColumns">����</param>
        /// <param name="lowIndex">��ʼ</param>
        /// <param name="topIndex">����</param>
        /// <returns>���ط�ҳ�����</returns>
        DataTable ExecuteDataTablePaged(string pagedTableName, string pagedSelectColumns, string pagedPrimaryKey, string whereCondition, List<DataParameter> arrParam, string pagedOrderColumns, int lowIndex, int topIndex);
        /// <summary>
        /// ��ѯ��ȡ��ҳ�ܼ�¼��
        /// </summary>
        /// <param name="pagedTableName">����</param>
        /// <param name="whereCondition">����</param>
        /// <param name="arrParam">��������</param>
        /// <returns>���ط�ҳ�ܼ�¼��</returns>
        DataTable ExecuteDataTablePaged(string pagedTableName, string whereCondition, List<DataParameter> arrParam);
    }
}
