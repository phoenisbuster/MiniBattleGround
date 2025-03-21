// Code generated by protoc-gen-go. DO NOT EDIT.
// versions:
// 	protoc-gen-go v1.27.1
// 	protoc        v3.19.1
// source: user_registration.proto

package registration

import (
	_ "github.com/envoyproxy/protoc-gen-validate/validate"
	domain "gitlab.com/muziverse/common/protogogenerator/user-proto-api/domain"
	social "gitlab.com/muziverse/common/protogogenerator/user-proto-api/social"
	wallet "gitlab.com/muziverse/common/protogogenerator/user-proto-api/wallet"
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

type RegistrationRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Provider domain.AuthenticationProvider `protobuf:"varint,1,opt,name=provider,proto3,enum=AuthenticationProvider" json:"provider,omitempty"`
	// Types that are assignable to Request:
	//	*RegistrationRequest_InternalRequest
	//	*RegistrationRequest_WalletRequest
	//	*RegistrationRequest_SocialRequest
	Request isRegistrationRequest_Request `protobuf_oneof:"request"`
}

func (x *RegistrationRequest) Reset() {
	*x = RegistrationRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_registration_proto_msgTypes[0]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *RegistrationRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*RegistrationRequest) ProtoMessage() {}

func (x *RegistrationRequest) ProtoReflect() protoreflect.Message {
	mi := &file_user_registration_proto_msgTypes[0]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use RegistrationRequest.ProtoReflect.Descriptor instead.
func (*RegistrationRequest) Descriptor() ([]byte, []int) {
	return file_user_registration_proto_rawDescGZIP(), []int{0}
}

func (x *RegistrationRequest) GetProvider() domain.AuthenticationProvider {
	if x != nil {
		return x.Provider
	}
	return domain.AuthenticationProvider(0)
}

func (m *RegistrationRequest) GetRequest() isRegistrationRequest_Request {
	if m != nil {
		return m.Request
	}
	return nil
}

func (x *RegistrationRequest) GetInternalRequest() *RegistrationInternalRequest {
	if x, ok := x.GetRequest().(*RegistrationRequest_InternalRequest); ok {
		return x.InternalRequest
	}
	return nil
}

func (x *RegistrationRequest) GetWalletRequest() *RegistrationWalletRequest {
	if x, ok := x.GetRequest().(*RegistrationRequest_WalletRequest); ok {
		return x.WalletRequest
	}
	return nil
}

func (x *RegistrationRequest) GetSocialRequest() *RegistrationSocialRequest {
	if x, ok := x.GetRequest().(*RegistrationRequest_SocialRequest); ok {
		return x.SocialRequest
	}
	return nil
}

type isRegistrationRequest_Request interface {
	isRegistrationRequest_Request()
}

type RegistrationRequest_InternalRequest struct {
	InternalRequest *RegistrationInternalRequest `protobuf:"bytes,2,opt,name=internalRequest,proto3,oneof"`
}

type RegistrationRequest_WalletRequest struct {
	WalletRequest *RegistrationWalletRequest `protobuf:"bytes,3,opt,name=walletRequest,proto3,oneof"`
}

type RegistrationRequest_SocialRequest struct {
	SocialRequest *RegistrationSocialRequest `protobuf:"bytes,4,opt,name=socialRequest,proto3,oneof"`
}

func (*RegistrationRequest_InternalRequest) isRegistrationRequest_Request() {}

func (*RegistrationRequest_WalletRequest) isRegistrationRequest_Request() {}

func (*RegistrationRequest_SocialRequest) isRegistrationRequest_Request() {}

// The request message containing the user's name.
type RegistrationInternalRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Email       string `protobuf:"bytes,1,opt,name=email,proto3" json:"email,omitempty"`
	DisplayName string `protobuf:"bytes,2,opt,name=displayName,proto3" json:"displayName,omitempty"`
}

func (x *RegistrationInternalRequest) Reset() {
	*x = RegistrationInternalRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_registration_proto_msgTypes[1]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *RegistrationInternalRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*RegistrationInternalRequest) ProtoMessage() {}

func (x *RegistrationInternalRequest) ProtoReflect() protoreflect.Message {
	mi := &file_user_registration_proto_msgTypes[1]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use RegistrationInternalRequest.ProtoReflect.Descriptor instead.
func (*RegistrationInternalRequest) Descriptor() ([]byte, []int) {
	return file_user_registration_proto_rawDescGZIP(), []int{1}
}

func (x *RegistrationInternalRequest) GetEmail() string {
	if x != nil {
		return x.Email
	}
	return ""
}

func (x *RegistrationInternalRequest) GetDisplayName() string {
	if x != nil {
		return x.DisplayName
	}
	return ""
}

type RegistrationWalletRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	WalletAddress string                `protobuf:"bytes,1,opt,name=walletAddress,proto3" json:"walletAddress,omitempty"`
	Network       domain.NetworkSupport `protobuf:"varint,2,opt,name=network,proto3,enum=NetworkSupport" json:"network,omitempty"`
}

func (x *RegistrationWalletRequest) Reset() {
	*x = RegistrationWalletRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_registration_proto_msgTypes[2]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *RegistrationWalletRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*RegistrationWalletRequest) ProtoMessage() {}

func (x *RegistrationWalletRequest) ProtoReflect() protoreflect.Message {
	mi := &file_user_registration_proto_msgTypes[2]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use RegistrationWalletRequest.ProtoReflect.Descriptor instead.
func (*RegistrationWalletRequest) Descriptor() ([]byte, []int) {
	return file_user_registration_proto_rawDescGZIP(), []int{2}
}

func (x *RegistrationWalletRequest) GetWalletAddress() string {
	if x != nil {
		return x.WalletAddress
	}
	return ""
}

func (x *RegistrationWalletRequest) GetNetwork() domain.NetworkSupport {
	if x != nil {
		return x.Network
	}
	return domain.NetworkSupport(0)
}

type RegistrationSocialRequest struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Token                 string                        `protobuf:"bytes,1,opt,name=token,proto3" json:"token,omitempty"`
	SocialUserInformation *social.SocialUserInformation `protobuf:"bytes,2,opt,name=socialUserInformation,proto3" json:"socialUserInformation,omitempty"`
}

func (x *RegistrationSocialRequest) Reset() {
	*x = RegistrationSocialRequest{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_registration_proto_msgTypes[3]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *RegistrationSocialRequest) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*RegistrationSocialRequest) ProtoMessage() {}

func (x *RegistrationSocialRequest) ProtoReflect() protoreflect.Message {
	mi := &file_user_registration_proto_msgTypes[3]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use RegistrationSocialRequest.ProtoReflect.Descriptor instead.
func (*RegistrationSocialRequest) Descriptor() ([]byte, []int) {
	return file_user_registration_proto_rawDescGZIP(), []int{3}
}

func (x *RegistrationSocialRequest) GetToken() string {
	if x != nil {
		return x.Token
	}
	return ""
}

func (x *RegistrationSocialRequest) GetSocialUserInformation() *social.SocialUserInformation {
	if x != nil {
		return x.SocialUserInformation
	}
	return nil
}

// The response message containing the greetings
type RegistrationResponse struct {
	state         protoimpl.MessageState
	sizeCache     protoimpl.SizeCache
	unknownFields protoimpl.UnknownFields

	Status                     domain.UserFlowStatus     `protobuf:"varint,1,opt,name=status,proto3,enum=UserFlowStatus" json:"status,omitempty"`
	AccessToken                string                    `protobuf:"bytes,2,opt,name=access_token,json=accessToken,proto3" json:"access_token,omitempty"`
	RefreshToken               string                    `protobuf:"bytes,3,opt,name=refresh_token,json=refreshToken,proto3" json:"refresh_token,omitempty"`
	SignatureVerificationToken string                    `protobuf:"bytes,4,opt,name=signature_verification_token,json=signatureVerificationToken,proto3" json:"signature_verification_token,omitempty"`
	OtpVerificationToken       string                    `protobuf:"bytes,5,opt,name=otp_verification_token,json=otpVerificationToken,proto3" json:"otp_verification_token,omitempty"`
	Challenge                  *wallet.SecurityChallenge `protobuf:"bytes,6,opt,name=challenge,proto3" json:"challenge,omitempty"`
}

func (x *RegistrationResponse) Reset() {
	*x = RegistrationResponse{}
	if protoimpl.UnsafeEnabled {
		mi := &file_user_registration_proto_msgTypes[4]
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		ms.StoreMessageInfo(mi)
	}
}

func (x *RegistrationResponse) String() string {
	return protoimpl.X.MessageStringOf(x)
}

func (*RegistrationResponse) ProtoMessage() {}

func (x *RegistrationResponse) ProtoReflect() protoreflect.Message {
	mi := &file_user_registration_proto_msgTypes[4]
	if protoimpl.UnsafeEnabled && x != nil {
		ms := protoimpl.X.MessageStateOf(protoimpl.Pointer(x))
		if ms.LoadMessageInfo() == nil {
			ms.StoreMessageInfo(mi)
		}
		return ms
	}
	return mi.MessageOf(x)
}

// Deprecated: Use RegistrationResponse.ProtoReflect.Descriptor instead.
func (*RegistrationResponse) Descriptor() ([]byte, []int) {
	return file_user_registration_proto_rawDescGZIP(), []int{4}
}

func (x *RegistrationResponse) GetStatus() domain.UserFlowStatus {
	if x != nil {
		return x.Status
	}
	return domain.UserFlowStatus(0)
}

func (x *RegistrationResponse) GetAccessToken() string {
	if x != nil {
		return x.AccessToken
	}
	return ""
}

func (x *RegistrationResponse) GetRefreshToken() string {
	if x != nil {
		return x.RefreshToken
	}
	return ""
}

func (x *RegistrationResponse) GetSignatureVerificationToken() string {
	if x != nil {
		return x.SignatureVerificationToken
	}
	return ""
}

func (x *RegistrationResponse) GetOtpVerificationToken() string {
	if x != nil {
		return x.OtpVerificationToken
	}
	return ""
}

func (x *RegistrationResponse) GetChallenge() *wallet.SecurityChallenge {
	if x != nil {
		return x.Challenge
	}
	return nil
}

var File_user_registration_proto protoreflect.FileDescriptor

var file_user_registration_proto_rawDesc = []byte{
	0x0a, 0x17, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74,
	0x69, 0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x12, 0x15, 0x69, 0x64, 0x65, 0x6e, 0x74,
	0x69, 0x74, 0x79, 0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e,
	0x1a, 0x29, 0x64, 0x6f, 0x6d, 0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x61, 0x75,
	0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x5f, 0x70, 0x72, 0x6f,
	0x76, 0x69, 0x64, 0x65, 0x72, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x1d, 0x64, 0x6f, 0x6d,
	0x61, 0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x66, 0x6c, 0x6f, 0x77, 0x5f, 0x73, 0x74,
	0x61, 0x74, 0x75, 0x73, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x21, 0x64, 0x6f, 0x6d, 0x61,
	0x69, 0x6e, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x6e, 0x65, 0x74, 0x77, 0x6f, 0x72, 0x6b, 0x5f,
	0x73, 0x75, 0x70, 0x70, 0x6f, 0x72, 0x74, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x29, 0x73,
	0x6f, 0x63, 0x69, 0x61, 0x6c, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x73, 0x6f, 0x63, 0x69, 0x61,
	0x6c, 0x5f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x69, 0x6e, 0x66, 0x6f, 0x72, 0x6d, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x1a, 0x17, 0x76, 0x61, 0x6c, 0x69, 0x64, 0x61,
	0x74, 0x65, 0x2f, 0x76, 0x61, 0x6c, 0x69, 0x64, 0x61, 0x74, 0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74,
	0x6f, 0x1a, 0x24, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x2f, 0x75, 0x73, 0x65, 0x72, 0x5f, 0x73,
	0x65, 0x63, 0x75, 0x72, 0x69, 0x74, 0x79, 0x5f, 0x63, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67,
	0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x22, 0xee, 0x02, 0x0a, 0x13, 0x52, 0x65, 0x67, 0x69,
	0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12,
	0x33, 0x0a, 0x08, 0x70, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x18, 0x01, 0x20, 0x01, 0x28,
	0x0e, 0x32, 0x17, 0x2e, 0x41, 0x75, 0x74, 0x68, 0x65, 0x6e, 0x74, 0x69, 0x63, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x76, 0x69, 0x64, 0x65, 0x72, 0x52, 0x08, 0x70, 0x72, 0x6f, 0x76,
	0x69, 0x64, 0x65, 0x72, 0x12, 0x5e, 0x0a, 0x0f, 0x69, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x61, 0x6c,
	0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x18, 0x02, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x32, 0x2e,
	0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x74, 0x79, 0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72,
	0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x61, 0x6c, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73,
	0x74, 0x48, 0x00, 0x52, 0x0f, 0x69, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x61, 0x6c, 0x52, 0x65, 0x71,
	0x75, 0x65, 0x73, 0x74, 0x12, 0x58, 0x0a, 0x0d, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x52, 0x65,
	0x71, 0x75, 0x65, 0x73, 0x74, 0x18, 0x03, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x30, 0x2e, 0x69, 0x64,
	0x65, 0x6e, 0x74, 0x69, 0x74, 0x79, 0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74,
	0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e,
	0x57, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x48, 0x00, 0x52,
	0x0d, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x58,
	0x0a, 0x0d, 0x73, 0x6f, 0x63, 0x69, 0x61, 0x6c, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x18,
	0x04, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x30, 0x2e, 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x74, 0x79,
	0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65,
	0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x53, 0x6f, 0x63, 0x69, 0x61, 0x6c,
	0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x48, 0x00, 0x52, 0x0d, 0x73, 0x6f, 0x63, 0x69, 0x61,
	0x6c, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x42, 0x0e, 0x0a, 0x07, 0x72, 0x65, 0x71, 0x75,
	0x65, 0x73, 0x74, 0x12, 0x03, 0xf8, 0x42, 0x01, 0x22, 0x6c, 0x0a, 0x1b, 0x52, 0x65, 0x67, 0x69,
	0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x61, 0x6c,
	0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x22, 0x0a, 0x05, 0x65, 0x6d, 0x61, 0x69, 0x6c,
	0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x42, 0x0c, 0xfa, 0x42, 0x09, 0x72, 0x07, 0x10, 0x05, 0x18,
	0xfe, 0x01, 0x60, 0x01, 0x52, 0x05, 0x65, 0x6d, 0x61, 0x69, 0x6c, 0x12, 0x29, 0x0a, 0x0b, 0x64,
	0x69, 0x73, 0x70, 0x6c, 0x61, 0x79, 0x4e, 0x61, 0x6d, 0x65, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09,
	0x42, 0x07, 0xfa, 0x42, 0x04, 0x72, 0x02, 0x10, 0x05, 0x52, 0x0b, 0x64, 0x69, 0x73, 0x70, 0x6c,
	0x61, 0x79, 0x4e, 0x61, 0x6d, 0x65, 0x22, 0x75, 0x0a, 0x19, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74,
	0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x57, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x52, 0x65, 0x71, 0x75,
	0x65, 0x73, 0x74, 0x12, 0x2d, 0x0a, 0x0d, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x41, 0x64, 0x64,
	0x72, 0x65, 0x73, 0x73, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x42, 0x07, 0xfa, 0x42, 0x04, 0x72,
	0x02, 0x10, 0x01, 0x52, 0x0d, 0x77, 0x61, 0x6c, 0x6c, 0x65, 0x74, 0x41, 0x64, 0x64, 0x72, 0x65,
	0x73, 0x73, 0x12, 0x29, 0x0a, 0x07, 0x6e, 0x65, 0x74, 0x77, 0x6f, 0x72, 0x6b, 0x18, 0x02, 0x20,
	0x01, 0x28, 0x0e, 0x32, 0x0f, 0x2e, 0x4e, 0x65, 0x74, 0x77, 0x6f, 0x72, 0x6b, 0x53, 0x75, 0x70,
	0x70, 0x6f, 0x72, 0x74, 0x52, 0x07, 0x6e, 0x65, 0x74, 0x77, 0x6f, 0x72, 0x6b, 0x22, 0x92, 0x01,
	0x0a, 0x19, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x53, 0x6f,
	0x63, 0x69, 0x61, 0x6c, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73, 0x74, 0x12, 0x1d, 0x0a, 0x05, 0x74,
	0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x01, 0x20, 0x01, 0x28, 0x09, 0x42, 0x07, 0xfa, 0x42, 0x04, 0x72,
	0x02, 0x10, 0x01, 0x52, 0x05, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x56, 0x0a, 0x15, 0x73, 0x6f,
	0x63, 0x69, 0x61, 0x6c, 0x55, 0x73, 0x65, 0x72, 0x49, 0x6e, 0x66, 0x6f, 0x72, 0x6d, 0x61, 0x74,
	0x69, 0x6f, 0x6e, 0x18, 0x02, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x16, 0x2e, 0x53, 0x6f, 0x63, 0x69,
	0x61, 0x6c, 0x55, 0x73, 0x65, 0x72, 0x49, 0x6e, 0x66, 0x6f, 0x72, 0x6d, 0x61, 0x74, 0x69, 0x6f,
	0x6e, 0x42, 0x08, 0xfa, 0x42, 0x05, 0x8a, 0x01, 0x02, 0x08, 0x01, 0x52, 0x15, 0x73, 0x6f, 0x63,
	0x69, 0x61, 0x6c, 0x55, 0x73, 0x65, 0x72, 0x49, 0x6e, 0x66, 0x6f, 0x72, 0x6d, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x22, 0xb1, 0x02, 0x0a, 0x14, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74,
	0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x12, 0x27, 0x0a, 0x06, 0x73,
	0x74, 0x61, 0x74, 0x75, 0x73, 0x18, 0x01, 0x20, 0x01, 0x28, 0x0e, 0x32, 0x0f, 0x2e, 0x55, 0x73,
	0x65, 0x72, 0x46, 0x6c, 0x6f, 0x77, 0x53, 0x74, 0x61, 0x74, 0x75, 0x73, 0x52, 0x06, 0x73, 0x74,
	0x61, 0x74, 0x75, 0x73, 0x12, 0x21, 0x0a, 0x0c, 0x61, 0x63, 0x63, 0x65, 0x73, 0x73, 0x5f, 0x74,
	0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x02, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0b, 0x61, 0x63, 0x63, 0x65,
	0x73, 0x73, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x23, 0x0a, 0x0d, 0x72, 0x65, 0x66, 0x72, 0x65,
	0x73, 0x68, 0x5f, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x03, 0x20, 0x01, 0x28, 0x09, 0x52, 0x0c,
	0x72, 0x65, 0x66, 0x72, 0x65, 0x73, 0x68, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x40, 0x0a, 0x1c,
	0x73, 0x69, 0x67, 0x6e, 0x61, 0x74, 0x75, 0x72, 0x65, 0x5f, 0x76, 0x65, 0x72, 0x69, 0x66, 0x69,
	0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x5f, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x04, 0x20, 0x01,
	0x28, 0x09, 0x52, 0x1a, 0x73, 0x69, 0x67, 0x6e, 0x61, 0x74, 0x75, 0x72, 0x65, 0x56, 0x65, 0x72,
	0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x54, 0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x34,
	0x0a, 0x16, 0x6f, 0x74, 0x70, 0x5f, 0x76, 0x65, 0x72, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69,
	0x6f, 0x6e, 0x5f, 0x74, 0x6f, 0x6b, 0x65, 0x6e, 0x18, 0x05, 0x20, 0x01, 0x28, 0x09, 0x52, 0x14,
	0x6f, 0x74, 0x70, 0x56, 0x65, 0x72, 0x69, 0x66, 0x69, 0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x54,
	0x6f, 0x6b, 0x65, 0x6e, 0x12, 0x30, 0x0a, 0x09, 0x63, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67,
	0x65, 0x18, 0x06, 0x20, 0x01, 0x28, 0x0b, 0x32, 0x12, 0x2e, 0x53, 0x65, 0x63, 0x75, 0x72, 0x69,
	0x74, 0x79, 0x43, 0x68, 0x61, 0x6c, 0x6c, 0x65, 0x6e, 0x67, 0x65, 0x52, 0x09, 0x63, 0x68, 0x61,
	0x6c, 0x6c, 0x65, 0x6e, 0x67, 0x65, 0x32, 0x87, 0x01, 0x0a, 0x17, 0x55, 0x73, 0x65, 0x72, 0x52,
	0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x53, 0x65, 0x72, 0x76, 0x69,
	0x63, 0x65, 0x12, 0x6c, 0x0a, 0x0f, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74, 0x65, 0x72, 0x4e, 0x65,
	0x77, 0x55, 0x73, 0x65, 0x72, 0x12, 0x2a, 0x2e, 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x74, 0x79,
	0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65,
	0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x71, 0x75, 0x65, 0x73,
	0x74, 0x1a, 0x2b, 0x2e, 0x69, 0x64, 0x65, 0x6e, 0x74, 0x69, 0x74, 0x79, 0x2e, 0x72, 0x65, 0x67,
	0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x2e, 0x52, 0x65, 0x67, 0x69, 0x73, 0x74,
	0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x52, 0x65, 0x73, 0x70, 0x6f, 0x6e, 0x73, 0x65, 0x22, 0x00,
	0x42, 0xb5, 0x01, 0x0a, 0x28, 0x69, 0x6f, 0x2e, 0x6d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73,
	0x65, 0x2e, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2e, 0x75, 0x73, 0x65, 0x72, 0x2e, 0x61, 0x70, 0x69,
	0x2e, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x42, 0x11, 0x52,
	0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x50, 0x72, 0x6f, 0x74, 0x6f,
	0x50, 0x01, 0x5a, 0x4c, 0x67, 0x69, 0x74, 0x6c, 0x61, 0x62, 0x2e, 0x63, 0x6f, 0x6d, 0x2f, 0x6d,
	0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2f, 0x63, 0x6f, 0x6d, 0x6d, 0x6f, 0x6e, 0x2f,
	0x70, 0x72, 0x6f, 0x74, 0x6f, 0x67, 0x6f, 0x67, 0x65, 0x6e, 0x65, 0x72, 0x61, 0x74, 0x6f, 0x72,
	0x2f, 0x75, 0x73, 0x65, 0x72, 0x2d, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x2d, 0x61, 0x70, 0x69, 0x2f,
	0x61, 0x70, 0x69, 0x2f, 0x72, 0x65, 0x67, 0x69, 0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e,
	0xaa, 0x02, 0x25, 0x4d, 0x75, 0x7a, 0x69, 0x76, 0x65, 0x72, 0x73, 0x65, 0x2e, 0x50, 0x72, 0x6f,
	0x74, 0x6f, 0x2e, 0x55, 0x73, 0x65, 0x72, 0x2e, 0x41, 0x70, 0x69, 0x2e, 0x52, 0x65, 0x67, 0x69,
	0x73, 0x74, 0x72, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x62, 0x06, 0x70, 0x72, 0x6f, 0x74, 0x6f, 0x33,
}

var (
	file_user_registration_proto_rawDescOnce sync.Once
	file_user_registration_proto_rawDescData = file_user_registration_proto_rawDesc
)

func file_user_registration_proto_rawDescGZIP() []byte {
	file_user_registration_proto_rawDescOnce.Do(func() {
		file_user_registration_proto_rawDescData = protoimpl.X.CompressGZIP(file_user_registration_proto_rawDescData)
	})
	return file_user_registration_proto_rawDescData
}

var file_user_registration_proto_msgTypes = make([]protoimpl.MessageInfo, 5)
var file_user_registration_proto_goTypes = []interface{}{
	(*RegistrationRequest)(nil),          // 0: identity.registration.RegistrationRequest
	(*RegistrationInternalRequest)(nil),  // 1: identity.registration.RegistrationInternalRequest
	(*RegistrationWalletRequest)(nil),    // 2: identity.registration.RegistrationWalletRequest
	(*RegistrationSocialRequest)(nil),    // 3: identity.registration.RegistrationSocialRequest
	(*RegistrationResponse)(nil),         // 4: identity.registration.RegistrationResponse
	(domain.AuthenticationProvider)(0),   // 5: AuthenticationProvider
	(domain.NetworkSupport)(0),           // 6: NetworkSupport
	(*social.SocialUserInformation)(nil), // 7: SocialUserInformation
	(domain.UserFlowStatus)(0),           // 8: UserFlowStatus
	(*wallet.SecurityChallenge)(nil),     // 9: SecurityChallenge
}
var file_user_registration_proto_depIdxs = []int32{
	5, // 0: identity.registration.RegistrationRequest.provider:type_name -> AuthenticationProvider
	1, // 1: identity.registration.RegistrationRequest.internalRequest:type_name -> identity.registration.RegistrationInternalRequest
	2, // 2: identity.registration.RegistrationRequest.walletRequest:type_name -> identity.registration.RegistrationWalletRequest
	3, // 3: identity.registration.RegistrationRequest.socialRequest:type_name -> identity.registration.RegistrationSocialRequest
	6, // 4: identity.registration.RegistrationWalletRequest.network:type_name -> NetworkSupport
	7, // 5: identity.registration.RegistrationSocialRequest.socialUserInformation:type_name -> SocialUserInformation
	8, // 6: identity.registration.RegistrationResponse.status:type_name -> UserFlowStatus
	9, // 7: identity.registration.RegistrationResponse.challenge:type_name -> SecurityChallenge
	0, // 8: identity.registration.UserRegistrationService.RegisterNewUser:input_type -> identity.registration.RegistrationRequest
	4, // 9: identity.registration.UserRegistrationService.RegisterNewUser:output_type -> identity.registration.RegistrationResponse
	9, // [9:10] is the sub-list for method output_type
	8, // [8:9] is the sub-list for method input_type
	8, // [8:8] is the sub-list for extension type_name
	8, // [8:8] is the sub-list for extension extendee
	0, // [0:8] is the sub-list for field type_name
}

func init() { file_user_registration_proto_init() }
func file_user_registration_proto_init() {
	if File_user_registration_proto != nil {
		return
	}
	if !protoimpl.UnsafeEnabled {
		file_user_registration_proto_msgTypes[0].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*RegistrationRequest); i {
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
		file_user_registration_proto_msgTypes[1].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*RegistrationInternalRequest); i {
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
		file_user_registration_proto_msgTypes[2].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*RegistrationWalletRequest); i {
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
		file_user_registration_proto_msgTypes[3].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*RegistrationSocialRequest); i {
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
		file_user_registration_proto_msgTypes[4].Exporter = func(v interface{}, i int) interface{} {
			switch v := v.(*RegistrationResponse); i {
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
	file_user_registration_proto_msgTypes[0].OneofWrappers = []interface{}{
		(*RegistrationRequest_InternalRequest)(nil),
		(*RegistrationRequest_WalletRequest)(nil),
		(*RegistrationRequest_SocialRequest)(nil),
	}
	type x struct{}
	out := protoimpl.TypeBuilder{
		File: protoimpl.DescBuilder{
			GoPackagePath: reflect.TypeOf(x{}).PkgPath(),
			RawDescriptor: file_user_registration_proto_rawDesc,
			NumEnums:      0,
			NumMessages:   5,
			NumExtensions: 0,
			NumServices:   1,
		},
		GoTypes:           file_user_registration_proto_goTypes,
		DependencyIndexes: file_user_registration_proto_depIdxs,
		MessageInfos:      file_user_registration_proto_msgTypes,
	}.Build()
	File_user_registration_proto = out.File
	file_user_registration_proto_rawDesc = nil
	file_user_registration_proto_goTypes = nil
	file_user_registration_proto_depIdxs = nil
}
