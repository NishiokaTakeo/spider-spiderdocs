library NSISArray;

{$mode objfpc}{$H+}

{
  NSISList Plugin - a simple List plugin for NSIS
  Written by LoRd_MuldeR <mulder2@gmx.de>

  based on: NSIS ExDLL example, by Peter Windridge

  Developed in Delphi 7.0 Professional, Tested with NSIS v2.37
}

uses
  Windows,
  SysUtils,
  Classes;

///////////////////////////////////////////////////////////////////////////////
// NSIS API Functions
///////////////////////////////////////////////////////////////////////////////

type
  VarConstants = (
    INST_0,
    INST_1,       // $1
    INST_2,       // $2
    INST_3,       // $3
    INST_4,       // $4
    INST_5,       // $5
    INST_6,       // $6
    INST_7,       // $7
    INST_8,       // $8
    INST_9,       // $9
    INST_R0,      // $R0
    INST_R1,      // $R1
    INST_R2,      // $R2
    INST_R3,      // $R3
    INST_R4,      // $R4
    INST_R5,      // $R5
    INST_R6,      // $R6
    INST_R7,      // $R7
    INST_R8,      // $R8
    INST_R9,      // $R9
    INST_CMDLINE, // $CMDLINE
    INST_INSTDIR, // $INSTDIR
    INST_OUTDIR,  // $OUTDIR
    INST_EXEDIR,  // $EXEDIR
    INST_LANG,    // $LANGUAGE
    __INST_LAST
    );
  TVariableList = INST_0..__INST_LAST;
  pstack_t = ^stack_t;
  stack_t = record
    next: pstack_t;
    text: PChar;
  end;

var
  g_stringsize: integer;
  g_stacktop: ^pstack_t;
  g_variables: PChar;
  g_hwndParent: HWND;

function PopString(): string;
var
  th: pstack_t;
begin
  if integer(g_stacktop^) <> 0 then begin
    th := g_stacktop^;

    // --- FIX ME --- //
    // Result := PChar(@th.text); // <-- COMPILE ERROR HERE
    // g_stacktop^ := th.next; // <-- COMPILE ERROR HERE
    // --- FIX Me --- //

    GlobalFree(HGLOBAL(th));
  end;
end;

procedure PushString(const str: string='');
var
  th: pstack_t;
begin
  if integer(g_stacktop) <> 0 then begin
    th := pstack_t(GlobalAlloc(GPTR, SizeOf(stack_t) + g_stringsize));

    // --- FIX ME --- //
    // lstrcpyn(@th.text, PChar(str), g_stringsize); // <-- COMPILE ERROR HERE
    // th.next := g_stacktop^; // <-- COMPILE ERROR HERE
    // --- FIX ME --- //

    g_stacktop^ := th;
  end;
end;

{function GetUserVariable(const varnum: TVariableList): string;
begin
  if (integer(varnum) >= 0) and (integer(varnum) < integer(__INST_LAST)) then
    Result := g_variables + integer(varnum) * g_stringsize
  else
    Result := '';
end;}

{procedure SetUserVariable(const varnum: TVariableList; const value: string);
begin
  if (value <> '') and (integer(varnum) >= 0) and (integer(varnum) < integer(__INST_LAST)) then
    lstrcpy(g_variables + integer(varnum) * g_stringsize, PChar(value))
end;}

///////////////////////////////////////////////////////////////////////////////
// Internal Functions
///////////////////////////////////////////////////////////////////////////////

var
  Storage: TStringList;
  Default: TStringList;

procedure GlobalInitialization(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer);
begin
  g_stringsize := string_size;
  g_hwndParent := hwndParent;
  g_stacktop := stacktop;
  g_variables := variables;
end;

procedure GlobalExceptionHandler;
begin
  FatalAppExit(0, 'NSISList: An unhandled exception has been detected !!!');
end;

function GetInstance(Key: String): TStringList;
var
  Index: Integer;
begin
  if (Storage = nil) or (Default = nil) then
  begin
    MessageBox(g_hwndParent, PAnsiChar('Error: Plugin was not initialized properly!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
    Result := nil;
    Exit;
  end;

  if Key = '' then
  begin
    Result := Default;
    Exit;
  end;

  Index := Storage.IndexOf(Key);

  if Index = -1 then
  begin
    MessageBox(g_hwndParent, PAnsiChar('Error: Cannot access list "' + Key + '", because it does not exist!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
    Result := nil;
    Exit;
  end;

  Result := Storage.Objects[Index] as TStringList;
end;

///////////////////////////////////////////////////////////////////////////////
// Plugin Functions
///////////////////////////////////////////////////////////////////////////////

// -------------------------- //
// --- Create and Destroy --- //
// -------------------------- //

procedure Create(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    if (Storage = nil) or (Default = nil) then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Plugin was not initialized properly!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    if Key = '' then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Cannot create list, because you did not specify a name!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    if Storage.IndexOf(Key) <> -1 then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Cannot create list "' + Key + '", because it already exist!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    Storage.AddObject(Key, TStringList.Create);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Destroy(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    if (Storage = nil) or (Default = nil) then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Plugin was not initialized properly!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    if Key = '' then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Cannot destroy list, because you did not specify a name!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    Storage.Delete(Storage.IndexOf(Key));
    List.Free;
  except
    GlobalExceptionHandler;
  end;
end;

// ------------------------ //
// --- List Read-Access --- //
// ------------------------ //

procedure Get(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index: Integer;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index := StrToIntDef(PopString, -1);

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    if (Index < 0) or (Index > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot get item, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      PushString('__ERROR');
      Exit;
    end;

    PushString(List[Index]);
  except
    GlobalExceptionHandler;
  end;
end;

procedure First(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    if List.Count < 1 then
    begin
      PushString('__EMPTY');
      Exit;
    end;

    PushString(List[0]);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Last(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    if List.Count < 1 then
    begin
      PushString('__EMPTY');
      Exit;
    end;

    PushString(List[List.Count-1]);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Count(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    PushString(IntToStr(List.Count));
  except
    GlobalExceptionHandler;
  end;
end;

procedure All(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    PushString('__LAST');

    if List.Count < 1 then
    begin
      Exit;
    end;

    for i := List.Count-1 downto 0 do
    begin
      PushString(List[i]);
    end;
  except
    GlobalExceptionHandler;
  end;
end;

procedure AllRev(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    PushString('__LAST');

    if List.Count < 1 then
    begin
      Exit;
    end;

    for i := 0 to List.Count-1 do
    begin
      PushString(List[i]);
    end;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Index(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Search: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Search := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    PushString(IntToStr(List.IndexOf(Search)));
  except
    GlobalExceptionHandler;
  end;
end;

procedure Copy(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index: Integer;
  Count: Integer;
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index := StrToIntDef(PopString, -1);
    Count := StrToIntDef(PopString, -1);

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    if (Index < 0) or (Index > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot get item, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      PushString('__ERROR');
      Exit;
    end;

    PushString('__LAST');

    if Count < 1 then
    begin
      Exit;
    end;

    if (Index + Count) > List.Count then
    begin
      Count := List.Count - Index;
    end;

    for i := (Index + Count - 1) downto Index do
    begin
      PushString(List[i]);
    end;
  except
    GlobalExceptionHandler;
  end;
end;

// ------------------------- //
// --- List Manipulation --- //
// ------------------------- //

procedure Add(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Item: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Item := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    List.Add(Item);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Append(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Strings: String;
  List: TStringList;
  Temp: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Strings := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    Temp := TStringList.Create;
    Temp.CommaText := Strings;
    List.AddStrings(Temp);
    Temp.Free;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Set_(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index: Integer;
  Item: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index := StrToIntDef(PopString, -1);
    Item := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if (Index < 0) or (Index > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot set item, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    List[Index] := Item;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Insert(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index: Integer;
  Item: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index := StrToIntDef(PopString, -1);
    Item := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if (List.Count < 1) then
    begin
      List.Add(Item);
      Exit;
    end;

    if (Index < 0) then
    begin
      Index := 0;
    end;

    if (Index > List.Count) then
    begin
      Index := List.Count;
    end;

    List.Insert(Index,Item);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Pop(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    if List.Count < 1 then
    begin
      PushString('__EMPTY');
      Exit;
    end;

    PushString(List[List.Count-1]);
    List.Delete(List.Count-1);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Delete(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index: Integer;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index := StrToIntDef(PopString, -1);

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if (Index < 0) or (Index > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot delete item, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    List.Delete(Index);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Clear(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    List.Clear;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Sort(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    List.Sort;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Exch(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Index1: Integer;
  Index2: Integer;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Index1 := StrToIntDef(PopString, -1);
    Index2 := StrToIntDef(PopString, -1);

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if (Index1 < 0) or (Index2 < 0) or (Index1 > List.Count-1) or (Index2 > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot exchange items, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    List.Exchange(Index1, Index2);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Move(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  IndexSource: Integer;
  IndexDestination: Integer;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    IndexSource := StrToIntDef(PopString, -1);
    IndexDestination := StrToIntDef(PopString, -1);

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if (IndexSource < 0) or (IndexSource > List.Count-1) then
    begin
      MessageBox(hwndParent, 'Cannot exchange items, because index is out of bounds!', 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    if IndexDestination < 0 then
    begin
      IndexDestination := 0;
    end;

    if IndexDestination > List.Count-1 then
    begin
      IndexDestination := List.Count-1;
    end;

    List.Move(IndexSource,IndexDestination);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Dim(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Size: Integer;
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Size := StrToIntDef(PopString, 0);

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if List.Count > Size then
    begin
      for i := List.Count-1 downto Size do
      begin
        List.Delete(i);
      end;
    end;

    if List.Count < Size then
    begin
      for i := 1 to (Size - List.Count) do
      begin
        List.Add('__NULL')
      end;
    end;
  except
    GlobalExceptionHandler;
  end;
end;

procedure Concat(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key_Source: String;
  Key_Destination: String;
  List_Source: TStringList;
  List_Destination: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key_Destination := PopString;
    Key_Source := PopString;

    List_Source := GetInstance(Key_Source);
    if List_Source = nil then
    begin
      Exit;
    end;

    List_Destination := GetInstance(Key_Destination);
    if List_Destination = nil then
    begin
      Exit;
    end;

    List_Destination.AddStrings(List_Source);
  except
    GlobalExceptionHandler;
  end;
end;

procedure Reverse(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      Exit;
    end;

    if List.Count < 1 then
    begin
      Exit;
    end;

    for i := 0 to ((List.Count div 2) - 1) do
    begin
      List.Exchange(i, List.Count - 1 - i);
    end;
  except
    GlobalExceptionHandler;
  end;
end;

// -------------------------- //
// --- Load and Save List --- //
// -------------------------- //

procedure Load(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Filename: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Filename := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    try
      List.LoadFromFile(Filename);
    except
      on E:Exception do
      begin
        MessageBox(hwndParent, PAnsiChar(E.Message), 'NSISList', MB_ICONWARNING or MB_TOPMOST);
        PushString('__ERROR');
        Exit;
      end;
    end;

    PushString('__SUCCESS');
  except
    GlobalExceptionHandler;
  end;
end;

procedure Save(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  Key: String;
  Filename: String;
  List: TStringList;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    Key := PopString;
    Filename := PopString;

    List := GetInstance(Key);
    if List = nil then
    begin
      PushString('__ERROR');
      Exit;
    end;

    try
      List.SaveToFile(Filename);
    except
      on E:Exception do
      begin
        MessageBox(hwndParent, PAnsiChar(E.Message), 'NSISList', MB_ICONWARNING or MB_TOPMOST);
        PushString('__ERROR');
        Exit;
      end;
    end;

    PushString('__SUCCESS');
  except
    GlobalExceptionHandler;
  end;
end;

// ----------------------- //
// --- For Developpers --- //
// ----------------------- //

procedure Debug(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  List: TStringList;
  i: Integer;
  p: PAnsiChar;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    if (Storage = nil) or (Default = nil) then
    begin
      MessageBox(hwndParent, 'Global storage no initialized yet!', 'NSISList', MB_ICONERROR or MB_TOPMOST);
      Exit;
    end;

    if IDNO = MessageBox(hwndParent, PAnsiChar('Display debug information now?'), 'NSISList', MB_ICONQUESTION or MB_YESNO or MB_TOPMOST or MB_DEFBUTTON2) then
    begin
      Exit;
    end;

    MessageBox(hwndParent, 'Now displaying the contents of the default list:', 'NSISList', MB_ICONINFORMATION or MB_TOPMOST);
    p := Default.GetText;
    MessageBox(hwndParent, p, 'NSISList', MB_TOPMOST);
    StrDispose(p);

    MessageBox(hwndParent, 'Now displaying all lists in the global storage:', 'NSISList', MB_ICONINFORMATION or MB_TOPMOST);
    p := Storage.GetText;
    MessageBox(hwndParent, p, 'NSISList', MB_TOPMOST);
    StrDispose(p);

    if Storage.Count > 0 then
    begin
      for i := 0 to Storage.Count-1 do
      begin
        try
          MessageBox(hwndParent, PAnsiChar('Now displaying the contents of the "' + Storage[i] + '" list:'), 'NSISList', MB_ICONINFORMATION or MB_TOPMOST);
          List := Storage.Objects[i] as TStringList;
          p := List.GetText;
          MessageBox(hwndParent, p, 'NSISList', MB_TOPMOST);
          StrDispose(p);
        except
          MessageBox(hwndParent, 'Error: cannot display this list!', 'NSISList', MB_ICONERROR or MB_TOPMOST);
        end;
      end;
    end;

    MessageBox(hwndParent, 'End of debug information.', 'NSISList', MB_ICONINFORMATION or MB_TOPMOST);
  except
    GlobalExceptionHandler;
  end;
end;

// --------------------- //
// --- Unload Plugin --- //
// --------------------- //

procedure Unload(const hwndParent: HWND; const string_size: integer; const variables: PChar; const stacktop: pointer); cdecl;
var
  List: TStringList;
  i: Integer;
begin
  try
    GlobalInitialization(hwndParent, string_size, variables, stacktop);

    if (Storage = nil) or (Default = nil) then
    begin
      MessageBox(hwndParent, PAnsiChar('Error: Plugin was not initialized properly!'), 'NSISList', MB_TOPMOST or MB_ICONERROR);
      Exit;
    end;

    if Storage.Count > 0 then
    begin
      for i := 0 to Storage.Count-1 do
      begin
        if Storage.Objects[i] <> nil then
        begin
          List := Storage.Objects[i] as TStringList;
          List.Free;
          Storage.Objects[i] := nil;
        end;
      end;
    end;

    Storage.Free;
    Storage := nil;
    Default.Free;
    Default := nil;
  except
    GlobalExceptionHandler;
  end;
end;

///////////////////////////////////////////////////////////////////////////////
// Exported Functions
///////////////////////////////////////////////////////////////////////////////

exports
  Add,
  All,
  AllRev,
  Append,
  Clear,
  Concat,
  Copy,
  Count,
  Create,
  Debug,
  Delete,
  Destroy,
  Dim,
  Exch,
  First,
  Get,
  Index,
  Insert,
  Last,
  Load,
  Move,
  Pop,
  Reverse,
  Save,
  Set_ name 'Set',
  Sort,
  Unload;

///////////////////////////////////////////////////////////////////////////////
// Initialization
///////////////////////////////////////////////////////////////////////////////

begin
  GlobalInitialization(0,0,nil,nil);

  Storage := TStringList.Create;
  Default := TStringList.Create;
end.


