// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: domain/user_wallet_balance.proto

package domain

import (
	protoreflect "google.golang.org/protobuf/reflect/protoreflect"
	protoimpl "google.golang.org/protobuf/runtime/protoimpl"
	reflect "reflect"
	sync "sync"
)

const (
	// Verify that this generated code is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(20 - protoimpl.MinVersion)
	// Verify that runtime/protoimpl is sufficiently up-to-date.
	_ = protoimpl.EnforceVersion(protoimpl.MaxVersion - 20)
)

type BalanceType int32

const (
	BalanceType_BALANCE           BalanceType = 0
	BalanceType_AVAILABLE_BALANCE BalanceType = 1
)

// Enum value maps for BalanceType.
var (
	BalanceType_name = map[int32]string{
		0: "BALANCE",
		1: "AVAILABLE_BALANCE",
	}
	BalanceType_value = map[string]int32{
		"BALANCE":           0,
		"AVAILABLE_BALANCE": 1,
	}
)

func (x BalanceType) Enum() *BalanceType {
	p := new(BalanceType)
	*p = x
	return p
}

func (x BalanceType) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (BalanceType) Descriptor() protoreflect.EnumDescriptor {
	return file_domain_user_wallet_balance_proto_enumTypes[0].Descriptor()
}

func (BalanceType) Type() protoreflect.EnumType {
	return &file_domain_user_wallet_balance_proto_enumTypes[0]
}

func (x BalanceType) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use BalanceType.Descriptor instead.
func (BalanceType) EnumDescriptor() ([]byte, []int) {
	return file_domain_user_wallet_balance_proto_rawDescGZIP(), []int{0}
}

type Operator int32

const (
	Operator_ADD      Operator = 0
	Operator_SUBTRACT Operator = 1
)

// Enum value maps for Operator.
var (
	Operator_name = map[int32]string{
		0: "ADD",
		1: "SUBTRACT",
	}
	Operator_value = map[string]int32{
		"ADD":      0,
		"SUBTRACT": 1,
	}
)

func (x Operator) Enum() *Operator {
	p := new(Operator)
	*p = x
	return p
}

func (x Operator) String() string {
	return protoimpl.X.EnumStringOf(x.Descriptor(), protoreflect.EnumNumber(x))
}

func (Operator) Descriptor() protoreflect.EnumDescriptor {
	return file_domain_user_wallet_balance_proto_enumTypes[1].Descriptor()
}

func (Operator) Type() protoreflect.EnumType {
	return &file_domain_user_wallet_balance_proto_enumTypes[1]
}

func (x Operator) Number() protoreflect.EnumNumber {
	return protoreflect.EnumNumber(x)
}

// Deprecated: Use Operator.Descriptor instead.
func (Operator) EnumDescriptor() ([]byte, []int) {
	return file_domain_user_wallet_balance_proto_rawDescGZIP(), []int{1}
}

type UpdateSingleBalanceRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Symbol         string      `protobuf:"bytes,1,opt,name=symbol,proto3" json:"symbol,omitempty"`
	Amount         string      `protobuf:"bytes,2,opt,name=amount,proto3" json:"amount,omitempty"`
	IsNeedToNotify bool        `protobuf:"varint,3,opt,name=is_need_to_notify,json=isNeedToNotify,proto3" json:"is_need_to_notify,omitempty"`
	Type           BalanceType `protobuf:"varint,4,opt,name=type,proto3,enum=BalanceType" json:"type,omitempty"`
	Operator       Operator    `protobuf:"varint,5,opt,name=operator,proto3,enum=Operator" json:"operator,omitempty"`
	UserId         string      `protobuf:"bytes,6,opt,name=user_id,json=userId,proto3" json:"user_id,omitempty"`
}

func (x *UpdateSingleBalanceRequest) Reset() {
	*x = UpdateSingleBalanceRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_user_wallet_balance_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *UpdateSingleBalanceRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*UpdateSingleBalanceRequest) ProtoMessage() {}

func (x *UpdateSingleBalanceRequest) ProtoReflect() protoreflect.Message {
	mi := &file_domain_user_wallet_balance_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use UpdateSingleBalanceRequest.ProtoReflect.Descriptor instead.
func (*UpdateSingleBalanceRequest) Descriptor() ([]byte, []int) {
	return file_domain_user_wallet_balance_proto_rawDescGZIP(), []int{0}
}

func (x *UpdateSingleBalanceRequest) GetSymbol() string {
	if x != nil {
		return x.Symbol
	}
	return ""
}

func (x *UpdateSingleBalanceRequest) GetAmount() string {
	if x != nil {
		return x.Amount
	}
	return ""
}

func (x *UpdateSingleBalanceRequest) GetIsNeedToNotify() bool {
	if x != nil {
		return x.IsNeedToNotify
	}
	return false
}

func (x *UpdateSingleBalanceRequest) GetType() BalanceType {
	if x != nil {
		return x.Type
	}
	return BalanceType_BALANCE
}

func (x *UpdateSingleBalanceRequest) GetOperator() Operator {
	if x != nil {
		return x.Operator
	}
	return Operator_ADD
}

func (x *UpdateSingleBalanceRequest) GetUserId() string {
	if x != nil {
		return x.UserId
	}
	return ""
}

type UpdateMultiBalancesRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	IsNeedToNotify bool                          `protobuf:"varint,1,opt,name=is_need_to_notify,json=isNeedToNotify,proto3" json:"is_need_to_notify,omitempty"`
	UserId         string                        `protobuf:"bytes,2,opt,name=user_id,json=userId,proto3" json:"user_id,omitempty"`
	Balance        []*UpdateSingleBalanceRequest `protobuf:"bytes,3,rep,name=balance,proto3" json:"balance,omitempty"`
}

func (x *UpdateMultiBalancesRequest) Reset() {
	*x = UpdateMultiBalancesRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_domain_user_wallet_balance_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *UpdateMultiBalancesRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*UpdateMultiBalancesRequest) ProtoMessage() {}

func (x *UpdateMultiBalancesRequest) ProtoReflect() protoreflect.Message {
	mi := &file_domain_user_wallet_balance_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use UpdateMultiBalancesRequest.ProtoReflect.Descriptor instead.
func (*UpdateMultiBalancesRequest) Descriptor() ([]byte, []int) {
	return file_domain_user_wallet_balance_proto_rawDescGZIP(), []int{1}
}

func (x *UpdateMultiBalancesRequest) GetIsNeedToNotify() bool {
	if x != nil {
		return x.IsNeedToNotify
	}
	return false
}

func (x *UpdateMultiBalancesRequest) GetUserId() string {
	if x != nil {
		return x.UserId
	}
	return ""
}

func (x *UpdateMultiBalancesRequest) GetBalance() []*UpdateSingleBalanceRequest {
	if x != nil {
		return x.Balance
	}
	return nil
}

var File_domain_user_wallet_balance_proto protoreflect.FileDescriptor

var file_domain_user_wallet_balance_proto_rawDesc = []byte{
	0x0a, 0x20, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x77, 0x61,
	0x6c, 0x6c, 0x65, 0x74, 0x5f, 0x62, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x2e, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x22, 0xd9, 0x01, 0x0a, 0x1a, 0x55, 0x70, 0x64, 0x61, 0x74, 0x65, 0x53, 0x69, 0x6e,
	0x67, 0x6c, 0x65, 0x42, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73,
	0x74, 0x12, 0x16, 0x0a, 0x06, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x18, 0x01, 0x20, 0x01, 0x28,
	0x09, 0x52, 0x06, 0x73, 0x79, 0x6d, 0x62, 0x6f, 0x6c, 0x12, 0x16, 0x0a, 0x06, 0x61, 0x6d, 0x6f,
	0x75, 0x6e, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x06, 0x61, 0x6d, 0x6f, 0x75, 0x6e,
	0x74, 0x12, 0x29, 0x0a, 0x11, 0x69, 0x73, 0x5f, 0x6e, 0x65, 0x65, 0x64, 0x5f, 0x74, 0x6f, 0x5f,
	0x6e, 0x6f, 0x74, 0x69, 0x66, 0x79, 0x18, 0x03, 0x20, 0x01, 0x28, 0x08, 0x52, 0x0e, 0x69, 0x73,
	0x4e, 0x65, 0x65, 0x64, 0x54, 0x6f, 0x4e, 0x6f, 0x74, 0x69, 0x66, 0x79, 0x12, 0x20, 0x0a, 0x04,
	0x74, 0x79, 0x70, 0x65, 0x18, 0x04, 0x20, 0x01, 0x28, 0x0e, 0x32, 0x0c, 0x2e, 0x42, 0x61, 0x6c,
	0x61, 0x6e, 0x63, 0x65, 0x54, 0x79, 0x70, 0x65, 0x52, 0x04, 0x74, 0x79, 0x70, 0x65, 0x12, 0x25,
	0x0a, 0x08, 0x6f, 0x70, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x18, 0x05, 0x20, 0x01, 0x28, 0x0e,
	0x32, 0x09, 0x2e, 0x4f, 0x70, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x52, 0x08, 0x6f, 0x70, 0x65,
	0x72, 0x61, 0x74, 0x6f, 0x72, 0x12, 0x17, 0x0a, 0x07, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x69, 0x64,
	0x18, 0x06, 0x20, 0x01, 0x28, 0x09, 0x52, 0x06, 0x75, 0x73, 0x65, 0x72, 0x49, 0x64, 0x22, 0x97,
	0x01, 0x0a, 0x1a, 0x55, 0x70, 0x64, 0x61, 0x74, 0x65, 0x4d, 0x75, 0x6c, 0x74, 0x69, 0x42, 0x61,
	0x6c, 0x61, 0x6e, 0x63, 0x65, 0x73, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x29, 0x0a,
	0x11, 0x69, 0x73, 0x5f, 0x6e, 0x65, 0x65, 0x64, 0x5f, 0x74, 0x6f, 0x5f, 0x6e, 0x6f, 0x74, 0x69,
	0x66, 0x79, 0x18, 0x01, 0x20, 0x01, 0x28, 0x08, 0x52, 0x0e, 0x69, 0x73, 0x4e, 0x65, 0x65, 0x64,
	0x54, 0x6f, 0x4e, 0x6f, 0x74, 0x69, 0x66, 0x79, 0x12, 0x17, 0x0a, 0x07, 0x75, 0x73, 0x65, 0x72,
	0x5f, 0x69, 0x64, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x06, 0x75, 0x73, 0x65, 0x72, 0x49,
	0x64, 0x12, 0x35, 0x0a, 0x07, 0x62, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x18, 0x03, 0x20, 0x03,
	0x28, 0x0b, 0x32, 0x1b, 0x2e, 0x55, 0x70, 0x64, 0x61, 0x74, 0x65, 0x53, 0x69, 0x6e, 0x67, 0x6c,
	0x65, 0x42, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x52,
	0x07, 0x62, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x2a, 0x31, 0x0a, 0x0b, 0x42, 0x61, 0x6c, 0x61,
	0x6e, 0x63, 0x65, 0x54, 0x79, 0x70, 0x65, 0x12, 0x0b, 0x0a, 0x07, 0x42, 0x41, 0x4c, 0x41, 0x4e,
	0x43, 0x45, 0x10, 0x00, 0x12, 0x15, 0x0a, 0x11, 0x41, 0x56, 0x41, 0x49, 0x4c, 0x41, 0x42, 0x4c,
	0x45, 0x5f, 0x42, 0x41, 0x4c, 0x41, 0x4e, 0x43, 0x45, 0x10, 0x01, 0x2a, 0x21, 0x0a, 0x08, 0x4f,
	0x70, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x12, 0x07, 0x0a, 0x03, 0x41, 0x44, 0x44, 0x10, 0x00,
	0x12, 0x0c, 0x0a, 0x08, 0x53, 0x55, 0x42, 0x54, 0x52, 0x41, 0x43, 0x54, 0x10, 0x01, 0x42, 0x8d,
	0x01, 0x0a, 0x22, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e,
	0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x61, 0x70, 0x69, 0x2e, 0x77,
	0x61, 0x6c, 0x6c, 0x65, 0x74, 0x42, 0x16, 0x55, 0x73, 0x65, 0x72, 0x57, 0x61, 0x6c, 0x6c, 0x65,
	0x74, 0x42, 0x61, 0x6c, 0x61, 0x6e, 0x63, 0x65, 0x50, 0x72, 0x6f, 0x74, 0x6f, 0x50, 0x01, 0x5a,
	0x4d, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d, 0x75, 0x7a, 0x69,
	0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f, 0x70, 0x72, 0x6f,
	0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72, 0x2f, 0x75, 0x73,
	0x65, 0x72, 0x2d, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d,
	0x61, 0x70, 0x69, 0x2f, 0x70, 0x6b, 0x67, 0x2f, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x62, 0x06,
	0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_domain_user_wallet_balance_proto_rawDescOnce sync.Once
	file_domain_user_wallet_balance_proto_rawDescData = file_domain_user_wallet_balance_proto_rawDesc
)

func file_domain_user_wallet_balance_proto_rawDescGZIP() []byte {
	file_domain_user_wallet_balance_proto_rawDescOnce.Do(func() {
		file_domain_user_wallet_balance_proto_rawDescData = protoimpl.X.CompressGZIP(file_domain_user_wallet_balance_proto_rawDescData)
	})
	return file_domain_user_wallet_balance_proto_rawDescData
}

var file_domain_user_wallet_balance_proto_enumTypes = make([]protoimpl.EnumInfo, 2)
var file_domain_user_wallet_balance_proto_msgTypes = make([]protoimpl.MessageInfo, 2)
var file_domain_user_wallet_balance_proto_goTypes = []interface{}{
	(BalanceType)(0),                   // 0: BalanceType
	(Operator)(0),                      // 1: Operator
	(*UpdateSingleBalanceRequest)(nil), // 2: UpdateSingleBalanceRequest
	(*UpdateMultiBalancesRequest)(nil), // 3: UpdateMultiBalancesRequest
}
var file_domain_user_wallet_balance_proto_depIdxs = []int32{
	0, // 0: UpdateSingleBalanceRequest.type:type_name -> BalanceType
	1, // 1: UpdateSingleBalanceRequest.operator:type_name -> Operator
	2, // 2: UpdateMultiBalancesRequest.balance:type_name -> UpdateSingleBalanceRequest
	3, // [3:3] is the sub-list for method output_type
	3, // [3:3] is the sub-list for method input_type
	3, // [3:3] is the sub-list for extension type_name
	3, // [3:3] is the sub-list for extension extendee
	0, // [0:3] is the sub-list for field type_name
}

func init() { file_domain_user_wallet_balance_proto_init() }
func file_domain_user_wallet_balance_proto_init() {
	if File_domain_user_wallet_balance_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_domain_user_wallet_balance_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*UpdateSingleBalanceRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
		file_domain_user_wallet_balance_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*UpdateMultiBalancesRequest); i {
			case 0:
				return &v.state
			case 1:
				return &v.sizeCache
			case 2:
				return &v.unknownFields
			default:
				return nil
			}
		}
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_domain_user_wallet_balance_proto_rawDesc,
			NumEnums:      2,
			NumMessages:   2,
			NumExtensions: 0,
			NumServices:   0,
		},
		GoTypes:           file_domain_user_wallet_balance_proto_goTypes,
		DependencyIndexes: file_domain_user_wallet_balance_proto_depIdxs,
		EnumInfos:         file_domain_user_wallet_balance_proto_enumTypes,
		MessageInfos:      file_domain_user_wallet_balance_proto_msgTypes,
	}.Build()
	File_domain_user_wallet_balance_proto = out.File
	file_domain_user_wallet_balance_proto_rawDesc = nil
	file_domain_user_wallet_balance_proto_goTypes = nil
	file_domain_user_wallet_balance_proto_depIdxs = nil
}
