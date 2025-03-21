// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: game_content_quest.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Muziverse.Proto.GameContent.Api.Quest {
  public static partial class GameContentQuestService
  {
    static readonly string __ServiceName = "gamecontent.quest.GameContentQuestService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest> __Marshaller_gamecontent_quest_QuestCreationRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Domain.QuestResponse> __Marshaller_QuestResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Domain.QuestResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest> __Marshaller_gamecontent_quest_ListQuestCreationRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> __Marshaller_ListQuestResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest> __Marshaller_gamecontent_quest_GetSingleQuestRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest> __Marshaller_gamecontent_quest_GetMultiQuestsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse> __Method_CreateSingleQuest = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateSingleQuest",
        __Marshaller_gamecontent_quest_QuestCreationRequest,
        __Marshaller_QuestResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> __Method_CreateMultiQuests = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CreateMultiQuests",
        __Marshaller_gamecontent_quest_ListQuestCreationRequest,
        __Marshaller_ListQuestResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse> __Method_GetSingleQuest = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSingleQuest",
        __Marshaller_gamecontent_quest_GetSingleQuestRequest,
        __Marshaller_QuestResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> __Method_GetMultiQuests = new grpc::Method<global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetMultiQuests",
        __Marshaller_gamecontent_quest_GetMultiQuestsRequest,
        __Marshaller_ListQuestResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Muziverse.Proto.GameContent.Api.Quest.GameContentQuestReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GameContentQuestService</summary>
    [grpc::BindServiceMethod(typeof(GameContentQuestService), "BindService")]
    public abstract partial class GameContentQuestServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.QuestResponse> CreateSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> CreateMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.QuestResponse> GetSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> GetMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GameContentQuestService</summary>
    public partial class GameContentQuestServiceClient : grpc::ClientBase<GameContentQuestServiceClient>
    {
      /// <summary>Creates a new client for GameContentQuestService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameContentQuestServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GameContentQuestService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GameContentQuestServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameContentQuestServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GameContentQuestServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.QuestResponse CreateSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSingleQuest(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.QuestResponse CreateSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateSingleQuest, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.QuestResponse> CreateSingleQuestAsync(global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateSingleQuestAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.QuestResponse> CreateSingleQuestAsync(global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateSingleQuest, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.ListQuestResponse CreateMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateMultiQuests(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.ListQuestResponse CreateMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CreateMultiQuests, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> CreateMultiQuestsAsync(global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CreateMultiQuestsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> CreateMultiQuestsAsync(global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CreateMultiQuests, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.QuestResponse GetSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleQuest(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.QuestResponse GetSingleQuest(global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSingleQuest, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.QuestResponse> GetSingleQuestAsync(global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleQuestAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.QuestResponse> GetSingleQuestAsync(global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSingleQuest, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.ListQuestResponse GetMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMultiQuests(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Muziverse.Proto.GameContent.Domain.ListQuestResponse GetMultiQuests(global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetMultiQuests, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> GetMultiQuestsAsync(global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetMultiQuestsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Muziverse.Proto.GameContent.Domain.ListQuestResponse> GetMultiQuestsAsync(global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetMultiQuests, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GameContentQuestServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GameContentQuestServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GameContentQuestServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CreateSingleQuest, serviceImpl.CreateSingleQuest)
          .AddMethod(__Method_CreateMultiQuests, serviceImpl.CreateMultiQuests)
          .AddMethod(__Method_GetSingleQuest, serviceImpl.GetSingleQuest)
          .AddMethod(__Method_GetMultiQuests, serviceImpl.GetMultiQuests).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GameContentQuestServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CreateSingleQuest, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Quest.QuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse>(serviceImpl.CreateSingleQuest));
      serviceBinder.AddMethod(__Method_CreateMultiQuests, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Quest.ListQuestCreationRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse>(serviceImpl.CreateMultiQuests));
      serviceBinder.AddMethod(__Method_GetSingleQuest, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Quest.GetSingleQuestRequest, global::Muziverse.Proto.GameContent.Domain.QuestResponse>(serviceImpl.GetSingleQuest));
      serviceBinder.AddMethod(__Method_GetMultiQuests, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Muziverse.Proto.GameContent.Api.Quest.GetMultiQuestsRequest, global::Muziverse.Proto.GameContent.Domain.ListQuestResponse>(serviceImpl.GetMultiQuests));
    }

  }
}
#endregion
