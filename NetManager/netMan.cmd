@echo   off

REM /************Ԥ������************/

set pwdLogFileName=pwdLog.txt

set exeLogFileName=exeLog.txt

set exeListFileName=exeList.txt

set pwdListFileName=pwdList.txt

setlocal enabledelayedexpansion

REM /************Ԥ�������************/


title ������������(С�ȶ�����:171882044)
color 8e
:begin
  cls
  echo �q�������������������������������������������������������r
  echo ����������������������������������������������������������
  echo ��������������       �����������˵���              ��   ��
  echo ������********************************************��������
  echo �������������� ��     ����                              ��
  echo �������������������� 1:�ֹ������޸�           ������  ����
  echo ��������������       2:�ļ������޸�                     ��
  echo �������������������� 3:�鿴�޸Ľ��                     ��
  echo ��������������       4:�ֹ�����ִ��                     ��
��echo ��            ����   5:�ļ�����ִ��                     ��
  echo �������������� ��    6:�鿴ִ�н��                     ��
  echo �������������������� 0:�˳�                             ��
  echo ��������������       �������������������������������� ����
  echo ����������������������������������������������������������
  echo �t�������������������������������������������������������s
  echo.
  set choice=
  set /p choice=��ѡ��(0~1):
  if /i "%choice%"=="1" goto changPwd
  if /i "%choice%"=="2" goto pwdListDetail
  if /i "%choice%"=="3" goto showPwdFile
  if /i "%choice%"=="4" goto execFile
  if /i "%choice%"=="5" goto exeListDetail
  if /i "%choice%"=="6" goto showExecFile
  if /i "%choice%"=="0" goto end
  echo.
  goto begin


  :changPwd
   set /p beginAddress=��ʼ��ַ(��:192.168.0.1):
   set /p endAddress=������ַ(��:254):
   set /p rootAdmin=��Ȩ��½�û�:
   set /p rootPwd=��Ȩ��½����:
   set /p newUser=Ҫ�޸ĵ��û�:
   set /p newPwd=Ҫ�޸ĵ�����:
   goto pwdDetail


  :pwdDetail 
   cls
   ECHO �޸���������: %beginAddress% ~ %endAddress%
   ECHO ��Ȩ��½�û���%rootAdmin%
   ECHO ��Ȩ��½���룺%rootPwd%
   ECHO Ҫ�޸ĵ��û���%newUser%
   ECHO Ҫ�޸ĵ����룺%newPwd%
   set /p choice=ȷ��(Y/N):
   if /i "%choice%"=="Y" goto pwdOkay
   if /i "%choice%"=="N" goto begin
   echo.
   goto pwdDetail


  :pwdOkay
   cls
   ECHO ����ô���ȳԸ�ѩ���,������Ϊ�������޸�����...
   ECHO OFF
   if exist "%pwdLogFileName%" del %pwdLogFileName%  
   for /F "tokens=1,2,3,4 delims=. " %%e in ('echo %beginAddress%') do set beginAddress=%%e.%%f.%%g&& set beginAdd=%%h 
   for /L %%e in (%beginAdd%,1,%endAddress%) do ( 
   cls 
   echo.    
   echo �����޸ĵ�Ip:%beginAddress%.%%e
   echo Ip:%beginAddress%.%%e:>>%pwdLogFileName%
   pwd \\%beginAddress%.%%e -u %rootAdmin% -p %rootPwd% %newUser% %newPwd%>>%pwdLogFileName%
   if ERRORLEVEL 0 (echo �޸ĳɹ�!>>%pwdLogFileName%) else (echo Ip����ʧ�ܻ��½���û�����������Ҫ���ĵ��û���������!>>%pwdLogFileName%)
   echo *****************************************************************>>%pwdLogFileName%
   )
   echo.
   echo.
   echo ����,ִ�����!
   echo.
   pause    
   goto begin  
 

  :showPwdFile
   echo.
   if not exist "%pwdLogFileName%" (echo �ܱ�Ǹ,�ļ�������!) else (explorer %pwdLogFileName%)
   echo.
   pause  
   goto begin


  :execFile
   set /p beginAddress=��ʼ��ַ(��:192.168.0.1):
   set /p endAddress=������ַ(��:254):
   set /p rootAdmin=��Ȩ��½�û�:
   set /p rootPwd=��Ȩ��½����:
   set /p filePath=Ҫ�ϴ���ִ�е��ļ�·��(��:c:\abc.exe):
   if not exist "%filePath%" echo �ļ�������! & pause & goto begin
   goto execDetail
   

   :execDetail
    cls
    ECHO ִ���ļ�����: %beginAddress% ~ %endAddress%
    ECHO ��Ȩ��½�û���%rootAdmin%
    ECHO ��Ȩ��½���룺%rootPwd%
    ECHO �����ļ�·����%filePath%
    set /p choice=ȷ��(Y/N):
    if /i "%choice%"=="Y" goto execOkay
    if /i "%choice%"=="N" goto begin
    echo.
    goto execDetail


  :execOkay
   cls
   ECHO �������Ȼ����Ȱ�,�����ϴ���ִ���ļ�...
   if /i exist "%exeLogFileName%" del %exeLogFileName%
   for /F "tokens=1,2,3,4 delims=. " %%e in ('echo %beginAddress%') do set beginAddress=%%e.%%f.%%g&& set beginAdd=%%h 
   for /L %%e in (%beginAdd%,1,%endAddress%) do (cls
   echo ����ִ�е�Ip:%beginAddress%.%%e 
   echo Ip:%beginAddress%.%%e:>>%exeLogFileName%   
   call :subexe %beginAddress%.%%e %rootAdmin% %rootPwd% %filePath% %exeLogFileName%
   echo *****************************************************************>>%exeLogFileName%
   )
   echo.
   echo.
   echo ŶҲ,�㶨��!
   echo.
   pause    
   goto begin  



  :showExecFile
   echo.
   if not exist "%exeLogFileName%" (echo �ܱ�Ǹ,�ļ�������!) else (explorer %exeLogFileName%)
   echo.
   pause
   goto begin

  
  :pwdListDetail
   set /p pwdListFileName=�������ļ�·��(��:c:\pwdList.txt):
   if not exist "%pwdListFileName%" echo �ļ�������! & pause & goto begin
   cls
   for /F "eol=* tokens=*" %%e in (%pwdListFileName%) do echo %%e
   set /p choice=ȷ��(Y/N):
   if /i "%choice%"=="Y" goto pwdListOkay
   if /i "%choice%"=="N" goto begin


  :pwdListOkay
   if exist "%pwdLogFileName%" del %pwdLogFileName%
   for /F "eol=* tokens=1,2,3,4,5 delims=,| " %%e in (%pwdListFileName%) do (cls
   echo.    
   echo �����޸ĵ�Ip:%%e
   echo Ip:%%e:>>%pwdLogFileName%
   pwd \\%%e -u %%f -p %%g %%h %%i>>%pwdLogFileName%
   if ERRORLEVEL 0 (echo �޸ĳɹ�!>>%pwdLogFileName%) else (echo Ip����ʧ�ܻ��½���û�����������Ҫ���ĵ��û���������!>>%pwdLogFileName%)
   echo *****************************************************************>>%pwdLogFileName%
   )
   echo.
   echo.
   echo ȫ��ִ�����!
   echo.
   pause    
   goto begin


  :exeListDetail
   set /p exeListFileName=�������ļ�·��(��:c:\exeList.txt):
   if not exist "%exeListFileName%" echo �ļ�������! & pause & goto begin
   cls
   for /F "eol=* tokens=*" %%e in (%exeListFileName%) do echo %%e
   set /p choice=ȷ��(Y/N):
   if /i "%choice%"=="Y" goto exeListOkay
   if /i "%choice%"=="N" goto begin


  :exeListOkay
   cls
   ECHO �������Ȼ����Ȱ�,�����ϴ���ִ���ļ�...
   if /i exist "%exeLogFileName%" del %exeLogFileName%
   for /F "eol=* tokens=1,2,3,4 delims=,| " %%e in (%exeListFileName%) do (cls
   echo ����ִ�е�Ip:%%e 
   echo Ip:%%e:>>%exeLogFileName%   
   call :subexe %%e %%f %%g %%h %exeLogFileName%
   echo *****************************************************************>>%exeLogFileName%
   )
   echo.
   echo.
   echo ŶҲ,�㶨��!
   echo.
   pause    
   goto begin  


  
  :end 
   exit  

  :subexe
  pexe \\%1 -u %2 -p %3 -c -f -d "%4"
  if %ERRORLEVEL%==-1 (echo ִ�е��ļ�������.>>%5) else (if %ERRORLEVEL%==53 (echo ���ӵ�IPʧ��.>>%5) else (if %ERRORLEVEL%==997 (echo Ŀ��IPδ����admin$����.>>%5) else (if %ERRORLEVEL%==1326 (echo �û������������.>>%5) else (echo ִ�гɹ�,����IDΪ%ERRORLEVEL%.>>%5)))) 
  goto :eof 